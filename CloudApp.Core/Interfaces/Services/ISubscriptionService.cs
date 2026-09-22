using CloudApp.Core.Dtos.Subscription;
using CloudApp.Core.Entities;
using CloudApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApp.Core.Interfaces.Services
{
    public interface ISubscriptionService
    {
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="targetId"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        Task<SubscriptionResult> SubscribeAsync(int userId, int targetId, SubscriptionTargetType targetType, CancellationToken ct = default);

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="targetId"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        Task<UnsubscriptionResult> UnsubscribeAsync(int userId, int targetId, SubscriptionTargetType targetType, CancellationToken ct = default);

        /// <summary>
        /// 用户订阅列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<List<UserSubscription>> GetSubscriptionsByUserAsync(int userId, CancellationToken ct = default);

        /// <summary>
        /// 是否已订阅目标
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="targetId"></param>
        /// <param name="targetType"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<bool> IsSubscribedAsync(int userId, int targetId, SubscriptionTargetType targetType, CancellationToken ct = default);
    }
}
