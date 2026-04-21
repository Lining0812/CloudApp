using CloudApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CloudApp.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public IdentityController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<ActionResult> Create()
        {
            if(!await _roleManager.RoleExistsAsync("Admin"))
            {
                AppRole role = new AppRole { Name = "Admin" };
                var result = await _roleManager.CreateAsync(role);
                if (!result.Succeeded) return BadRequest("角色创建失败");
            }

            var user = await _userManager.FindByNameAsync("yzk");
            if(user == null)
            {
                user = new AppUser { UserName = "yzk" };
                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded) return BadRequest("用户创建失败");
            }

            if(!await _userManager.IsInRoleAsync(user, "Adimin"))
            {
                var result = await _userManager.AddToRoleAsync(user, "Admin");
                if (!result.Succeeded) return BadRequest("用户添加角色失败");
            }

            return Ok("角色创建成功");
        }
    }
}
