using CloudApp.Core.Dtos.Subscription;
using CloudApp.Core.Entities;
using CloudApp.Core.Enums;
using CloudApp.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudApp.WebApi.Controllers
{
    /// <summary>
    /// 订阅接口
    /// </summary>
    [Route("api/[controller]")]
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
        public async Task<ActionResult<List<UserSubscription>>> GetMySubscriptionsAsync(CancellationToken ct)
        {
            var userId = GetUserId();
            var result = await _service.GetSubscriptionsByUserAsync(userId, ct);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<SubscriptionResult>> SubscribeAsync(int targetId, SubscriptionTargetType targetType, CancellationToken ct)
        {
            var userId = GetUserId();
            var result = await _service.SubscribeAsync(userId, targetId, targetType, ct);

            if(!result.Success) return BadRequest(result);

            return result.AlreadySubscribed ? Ok(result) : StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpDelete]
        public async Task<ActionResult<UnsubscriptionResult>> UnsubscribeAsync(int targetId, SubscriptionTargetType targetType, CancellationToken ct)
        {
            var userId = GetUserId();
            var result = await _service.UnsubscribeAsync(userId, targetId, targetType, ct);
            if (!result.Success) return BadRequest(result);
            return result.WasSubscribed ? Ok(result) : StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// 从Claims中获取用户ID
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private int GetUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId");
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new Exception("User ID claim is missing or invalid.");
            }
            return userId;

        }
    }
}
