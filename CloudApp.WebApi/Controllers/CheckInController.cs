using CloudApp.Core.Dtos.CheckIn;
using CloudApp.Core.Exceptions;
using CloudApp.Core.Interfaces.Services;
using CloudApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CloudApp.WebApi.Controllers
{
    /// <summary>
    /// 签到接口。
    /// 设计原则：签到主体（openId）与签到日期全部由服务端推导，
    /// 客户端不得传入任何身份或日期参数——否则「给谁签到」「签哪天」就交给了调用方，可被伪造。
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CheckInController : ControllerBase
    {
        private readonly ICheckInService _checkInService;
        private readonly UserManager<AppUser> _userManager;

        public CheckInController(ICheckInService checkInService, UserManager<AppUser> userManager)
        {
            _checkInService = checkInService;
            _userManager = userManager;
        }

        /// <summary>
        /// 今日签到。
        /// 幂等：重复调用返回 200，CheckInResult.AlreadyCheckIn = true（不是错误，也不返回 409）。
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CheckInResult>> CheckInAsync(CancellationToken ct)
        {
            var openId = await ResolveOpenIdAsync();
            var result = await _checkInService.CheckInAsync(openId, ct);
            return Ok(result);
        }

        /// <summary>
        /// 查询签到状态：今日是否已签 / 当前连续天数 / 累计天数 / 最近一次签到日。
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<CheckInStatus>> GetStatus(CancellationToken ct)
        {
            var openId = await ResolveOpenIdAsync();
            return Ok(await _checkInService.GetStatusAsync(openId, ct));
        }

        /// <summary>
        /// 从登录态反查 openId：JWT 的 NameIdentifier 是 AppUser.Id，需再取 WeChatOpenId。
        /// 用 GET 缓存语义无关的只读推导，失败一律抛 BusinessException（中间件转 400）。
        /// </summary>
        private async Task<string> ResolveOpenIdAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
                throw new BusinessException("登录态无效，请重新登录");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new BusinessException("登录态无效，请重新登录");

            if (string.IsNullOrWhiteSpace(user.WeChatOpenId))
                throw new BusinessException("当前账号未绑定微信，无法签到");

            return user.WeChatOpenId;
        }
    }
}
