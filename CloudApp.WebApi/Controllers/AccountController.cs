using CloudApp.Core.Confige;
using CloudApp.Core.Enums;
using CloudApp.Infrastructure.Extensions;
using CloudApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CloudApp.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IOptionsSnapshot<JwtSetting> _jwtSetting;

        public AccountController(
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IOptionsSnapshot<JwtSetting> jwtSetting
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSetting = jwtSetting;
        }

        [HttpPost]
        public async Task<ActionResult> Create()
        {
            if (!await _roleManager.RoleExistsAsync(RoleType.Admin.ToString()))
            {
                AppRole role = new AppRole { Name = RoleType.Admin.ToString() };
                var result = await _roleManager.CreateAsync(role);
                if (!result.Succeeded) return BadRequest("角色创建失败");
            }

            var user = await _userManager.FindByNameAsync("yzk");

            if (user == null)
            {
                user = new AppUser { UserName = "yzk" };
                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded) return BadRequest("用户创建失败");
            }

            if (!await _userManager.IsInRoleAsync(user, RoleType.Admin.ToString()))
            {
                var result = await _userManager.AddToRoleAsync(user, RoleType.Admin.ToString());
                if (!result.Succeeded) return BadRequest("用户添加角色失败");
            }

            return Ok("用户创建成功,并添加至角色中");
        }

        [HttpPost("{phoneNumber}/{email}")]
        public async Task<ActionResult> Register(string phoneNumber, string email)
        {
            if (await _userManager.IsPhoneNumberRegisteredAsync(phoneNumber)) return BadRequest("用户已存在");
            if (await _userManager.FindByEmailAsync(email) != null) return BadRequest("用户已存在");

            var user = new AppUser
            {
                UserName = phoneNumber,
                PhoneNumber = phoneNumber,
                Email = email
            };

            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded) return BadRequest("用户注册失败");

            if (!await _roleManager.RoleExistsAsync(RoleType.User.ToString()))
            {
                var role = new AppRole { Name = RoleType.User.ToString() };
                var roleResult = await _roleManager.CreateAsync(role);
                if (!roleResult.Succeeded) return BadRequest("角色创建失败");
            }

            await _userManager.AddToRoleAsync(user, RoleType.User.ToString());

            return Ok("用户注册成功");
        }

        [HttpPost]
        public async Task<ActionResult> SendResetPasswordToken(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return BadRequest();
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            Console.WriteLine($"你的验证码是：{token}");
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> ResetPassword(string userName, string token, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return BadRequest();
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (result.Succeeded)
            {
                await _userManager.ResetAccessFailedCountAsync(user);
                return Ok("密码重置成功");
            }
            await _userManager.AccessFailedAsync(user);
            var count = await _userManager.GetAccessFailedCountAsync(user);
            Console.WriteLine($"失败次数{count}");
            return BadRequest();
        }

        [HttpPost]
        public ActionResult<string> LoginTest(string userName, string password)
        {
            if (userName == "lyn" && password == "123456")
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, "1"));
                claims.Add(new Claim(ClaimTypes.Name, userName));

                string key = _jwtSetting.Value.SecKey;
                DateTime expire = DateTime.UtcNow.AddSeconds(_jwtSetting.Value.ExpireSeconds);

                byte[] secBytes = Encoding.UTF8.GetBytes(key);
                var secKey = new SymmetricSecurityKey(secBytes);
                var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);
                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: expire,
                    signingCredentials: credentials
                );
                string jwt = new JwtSecurityTokenHandler().WriteToken(token);

                return jwt;
            }
            else
            {
                return BadRequest("登录失败");
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return BadRequest("用户不存在");

            var success = await _userManager.CheckPasswordAsync(user, password);
            if (!success) return BadRequest("用户登录失败");

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = JwtTokenBuilder.BuildToken(claims, _jwtSetting.Value);

            //Response.Cookies.Append("token", token, new CookieOptions
            //{
            //    HttpOnly = true,
            //    Secure = true,
            //    SameSite = SameSiteMode.Strict,
            //    Expires = DateTimeOffset.UtcNow.AddSeconds(_jwtSetting.Value.ExpireSeconds)
            //});

            return Ok(token);
        }


    }
}
