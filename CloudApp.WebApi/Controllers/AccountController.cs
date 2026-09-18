using CloudApp.Core.Dtos.Account;
using CloudApp.Core.Dtos.WeChat;
using CloudApp.Core.Exceptions;
using CloudApp.Core.Interfaces.Services;
using CloudApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CloudApp.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService, UserManager<AppUser> userManager)
        {
            _accountService = accountService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAdminUser()
        {
            var result = await _accountService.CreateAdminUserAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromForm]RegisterRequest request)
        {
            var result = await _accountService.RegisterAsync(request);
            return Ok(result);
        }

        [HttpPost("{phoneNumber}")]
        public async Task<ActionResult<string>> RegisterByPhone(string phoneNumber)
        {
            var result = await _accountService.RegisterByPhoneAsync(phoneNumber);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> SendResetPasswordToken(string userName)
        {
            var token = await _accountService.GeneratePasswordResetTokenAsync(userName);
            return Ok(token);
        }

        [HttpPut]
        public async Task<ActionResult> ResetPassword(string userName, string token, string newPassword)
        {
            var result = await _accountService.ResetPasswordAsync(userName, token, newPassword);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login(string userName, string password)
        {
            var token = await _accountService.LoginAsync(userName, password);
            return Ok(token);
        }

        [HttpPost]
        public async Task<ActionResult<WeChatLoginResponse>> WeChatLogin([FromBody] WeChatLoginRequest request)
        {
            var result = await _accountService.WeChatLoginAsync(request);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public ActionResult<string> GetUserInfo()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var user = _accountService.FindByIdAsync(userId);
            //if (user == null) return NotFound();
            return Ok(userId);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> TestAuthorize()
        {
            return Ok("您已获得授权");
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult> UpdateProfile([FromBody] UpdateUserProfileDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId)) return Unauthorized();

            var result = await _accountService.UpdateProfileAsync(userId, dto);
            return Ok(result);
        }
    }
}
