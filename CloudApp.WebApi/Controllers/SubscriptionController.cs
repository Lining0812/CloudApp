using CloudApp.Core.Dtos.Subscription;
using CloudApp.Core.Entities;
using CloudApp.Core.Enums;
using CloudApp.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CloudApp.WebApi.Controllers
{
    /// <summary>
    /// 订阅接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _service;
        private readonly ILogger<SubscriptionController> _logger;
        public SubscriptionController(ISubscriptionService service, ILogger<SubscriptionController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserSubscriptionDto>>> GetMySubscriptionsAsync(CancellationToken ct)
        {
            if (!TryGetUserId(out var userId)) return Unauthorized();
            var result = await _service.GetSubscriptionsByUserAsync(userId, ct);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<SubscriptionResult>> SubscribeAsync([FromBody]SubscribeRequest request, CancellationToken ct)
        {
            if (!TryGetUserId(out var userId)) return Unauthorized();
            var result = await _service.SubscribeAsync(userId, request.TargetId, request.TargetType, ct);

            if(!result.Success) return BadRequest(result);

            return result.AlreadySubscribed ? Ok(result) : StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpDelete]
        public async Task<ActionResult<UnsubscriptionResult>> UnsubscribeAsync([FromBody]UnsubscribeRequest request, CancellationToken ct)
        {
            if (!TryGetUserId(out var userId)) return Unauthorized();
            var result = await _service.UnsubscribeAsync(userId, request.TargetId, request.TargetType, ct);
            if (!result.Success) return BadRequest(result);
            return result.WasSubscribed ? Ok(result) : StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// 从认证信息中解析 UserId
        /// </summary>
        /// <returns></returns>
        private bool TryGetUserId(out int userId)
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(claim, out userId);
        }
    }
}
