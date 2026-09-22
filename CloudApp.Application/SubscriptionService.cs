using CloudApp.Core.Dtos.Subscription;
using CloudApp.Core.Entities;
using CloudApp.Core.Enums;
using CloudApp.Core.Interfaces.Repositories;
using CloudApp.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApp.Application
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _repo;
        private readonly ITargetValidator _targetValidator;
        private readonly ILogger<SubscriptionService> _logger;
        public SubscriptionService(ISubscriptionRepository repo, ITargetValidator targetValidator, ILogger<SubscriptionService> logger)
        {
            _repo = repo;
            _targetValidator = targetValidator;
            _logger = logger;
        }
        public async Task<List<UserSubscription>> GetSubscriptionsByUserAsync(int userId, CancellationToken ct = default)
            => await _repo.GetAllByUserAsync(userId, ct);

        public async Task<bool> IsSubscribedAsync(int userId, int targetId, SubscriptionTargetType targetType, CancellationToken ct = default)
            =>await _repo.IsSubscribedAsync(userId, targetId, targetType, ct);

        public async Task<SubscriptionResult> SubscribeAsync(int userId, int targetId, SubscriptionTargetType targetType, CancellationToken ct = default)
        {
            var existing = await _repo.GetSubscriptionAsync(userId, targetId, targetType, ct);

            if (existing != null)
            {
                _logger.LogDebug("重复订阅（已存在）userId={UserId} target={Type}:{TargetId}", userId, targetType, targetId);
                return new SubscriptionResult
                {
                    Success = true,
                    AlreadySubscribed = true,
                    TargetId = targetId,
                    TargetType = targetType,
                    Message = "已订阅"
                };
            }

            await EnsureTargetExistsAsync(targetId, targetType, ct);

            var entity = new UserSubscription
            {
                UserId = userId,
                TargetId = targetId,
                TargetType = targetType,
            };
            await _repo.AddAsync(entity);
            await _repo.SaveChangeAsync();

            _logger.LogInformation("订阅成功 userId={UserId} target={Type}:{TargetId}", userId, targetType, targetId);

            return new SubscriptionResult
            {
                Success = true,
                AlreadySubscribed = false,
                TargetId = targetId,
                TargetType = targetType,
                Message = "订阅成功"
            };
        }

        public async Task<UnsubscriptionResult> UnsubscribeAsync(int userId, int targetId, SubscriptionTargetType targetType, CancellationToken ct = default)
        {
            var existing = await _repo.GetSubscriptionAsync(userId, targetId, targetType, ct);
            if (existing == null)
            {
                return new UnsubscriptionResult
                {
                    Success = true,
                    WasSubscribed = false,
                    TargetId = targetId,
                    TargetType = targetType,
                    Message = "用户尚未订阅该目标"
                };
            }

            await _repo.DeleteAsync(existing);
            await _repo.SaveChangeAsync();

            _logger.LogInformation("退订成功 userId={UserId} target={Type}:{TargetId}", userId, targetType, targetId);

            return new UnsubscriptionResult
            {
                Success = true,
                WasSubscribed = true,
                TargetId = targetId,
                TargetType = targetType,
                Message = "已取消订阅"
            };
        }

        /// <summary>
        /// 存在性检查，确保目标存在
        /// </summary>
        /// <param name="targetId"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        private async Task EnsureTargetExistsAsync(int targetId, SubscriptionTargetType targetType,CancellationToken ct = default)
        {
            var exists = await _targetValidator.TargetExistsAsync(targetId, targetType, ct);
            if (!exists)
            {
                throw new InvalidOperationException($"目标不存在{targetType}:{targetId}");
            }
        }

    }
}
