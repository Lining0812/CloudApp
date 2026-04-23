using CloudApp.Core.Enums;
using CloudApp.Infrastructure.Extensions;
using CloudApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CloudApp.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
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
    }
}
