using CloudApp.Core.Confige;
using CloudApp.Core.Dtos.Account;
using CloudApp.Core.Enums;
using CloudApp.Core.Exceptions;
using CloudApp.Core.Interfaces;
using CloudApp.Core.Interfaces.Services;
using CloudApp.Infrastructure.Extensions;
using CloudApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace CloudApp.Application
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IOptionsSnapshot<JwtSetting> _jwtSetting;
        private readonly IWeChatService _weChatService;

        public AccountService(
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IOptionsSnapshot<JwtSetting> jwtSetting,
            IWeChatService weChatService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSetting = jwtSetting;
            _weChatService = weChatService;
        }

        public async Task<string> CreateAdminUserAsync()
        {
            await EnsureRoleExistsAsync(RoleType.Admin);

            var user = await _userManager.FindByNameAsync("lyn");

            if (user == null)
            {
                user = new AppUser { UserName = "lyn" };

                var result = await _userManager.CreateAsync(user, "123456");
                if (!result.Succeeded) throw new BusinessException("用户创建失败");
            }

            if (!await _userManager.IsInRoleAsync(user, RoleType.Admin.ToString()))
            {
                var result = await _userManager.AddToRoleAsync(user, RoleType.Admin.ToString());
                if (!result.Succeeded) throw new BusinessException("用户添加角色失败");
            }

            return "用户创建成功,并添加至角色中";
        }

        public async Task<string> RegisterAsync(RegisterRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            if (await _userManager.FindByNameAsync(request.UserName) != null)
                throw new BusinessException("用户已存在");
            if (await _userManager.IsPhoneNumberRegisteredAsync(request.PhoneNumber))
                throw new BusinessException("用户已存在");
            if (await _userManager.FindByEmailAsync(request.Email) != null)
                throw new BusinessException("用户已存在");

            var user = new AppUser
            {
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) throw new BusinessException("用户注册失败");

            await EnsureRoleExistsAsync(RoleType.User);

            var addResult = await _userManager.AddToRoleAsync(user, RoleType.User.ToString());
            if (!addResult.Succeeded) throw new BusinessException("用户添加角色失败");

            return "用户注册成功";
        }

        public async Task<string> RegisterByPhoneAsync(string phoneNumber, string password)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("手机号不能为空");

            if (!System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^1[3-9]\d{9}$"))
                throw new ArgumentException("手机号格式不正确");

            if (await _userManager.IsPhoneNumberRegisteredAsync(phoneNumber))
                throw new BusinessException("手机号已注册");

            var user = new AppUser
            {
                UserName = phoneNumber,
                PhoneNumber = phoneNumber,
            };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded) throw new BusinessException("注册失败");

            await EnsureRoleExistsAsync(RoleType.User);

            var addResult = await _userManager.AddToRoleAsync(user, RoleType.User.ToString());
            if (!addResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                throw new BusinessException("注册失败");
            }

            return "注册成功";
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) throw new BusinessException("用户不存在");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            Console.WriteLine($"你的验证码是：{token}");
            return token;
        }

        public async Task<string> ResetPasswordAsync(string userName, string token, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) throw new BusinessException("用户不存在");
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (result.Succeeded)
            {
                await _userManager.ResetAccessFailedCountAsync(user);
                return "密码重置成功";
            }
            await _userManager.AccessFailedAsync(user);
            var count = await _userManager.GetAccessFailedCountAsync(user);
            Console.WriteLine($"失败次数{count}");
            throw new BusinessException("密码重置失败");
        }

        public async Task<string> LoginAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) throw new BusinessException("用户不存在");

            var success = await _userManager.CheckPasswordAsync(user, password);
            if (!success) throw new BusinessException("用户登录失败");

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return JwtTokenBuilder.BuildToken(claims, _jwtSetting.Value);
        }

        public string FindByIdAsync(string id)
        {
            var res = _userManager.FindByIdAsync(id).Result;
            return res?.UserName ?? "用户不存在";
        }

        public async Task<string> WeChatLoginAsync(string code)
        {
            var session = await _weChatService.Code2SessionAsync(code);
            if (session == null || session.errcode != 0)
                throw new BusinessException("微信登录失败：" + (session?.errmsg ?? "未知错误"));

            var user = await _userManager.FindByLoginAsync("WeChat", session.openid);
            if (user == null)
            {
                user = new AppUser
                {
                    UserName = "wx_" + session.openid[..12],
                    WeChatOpenId = session.openid,
                    WeChatUnionId = session.unionid,
                };
                await _userManager.CreateAsync(user);
                await EnsureRoleExistsAsync(RoleType.User);
                await _userManager.AddToRoleAsync(user, RoleType.User.ToString());
            }

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return JwtTokenBuilder.BuildToken(claims, _jwtSetting.Value);
        }

        private async Task EnsureRoleExistsAsync(RoleType roleType)
        {
            if (!await _roleManager.RoleExistsAsync(roleType.ToString()))
            {
                var role = new AppRole { Name = roleType.ToString() };
                var result = await _roleManager.CreateAsync(role);
                if (!result.Succeeded) throw new BusinessException("角色创建失败");
            }
        }
    }
}
