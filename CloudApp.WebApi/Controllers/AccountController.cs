using CloudApp.Core.Dtos.Account;
using CloudApp.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CloudApp.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAdminUser()
        {
            var result = await _accountService.CreateAdminUserAsync();
            return Ok(result);
        }

        [HttpPost("{phoneNumber}/{email}")]
        public async Task<ActionResult> Register(string phoneNumber, string email, string password)
        {
            var request = new RegisterRequest { UserName = phoneNumber, PhoneNumber = phoneNumber, Email = email, Password = password };
            var result = await _accountService.RegisterAsync(request);
            return Ok(result);
        }

        [HttpPost("{phoneNumber}")]
        public async Task<ActionResult> RegisterByPhone(string phoneNumber, string password)
        {
            var result = await _accountService.RegisterByPhoneAsync(phoneNumber, password);
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
        public async Task<ActionResult<string>> WeChatLogin(string code)
        {
            var token = await _accountService.WeChatLoginAsync(code);
            return Ok(token);
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
    }
}
