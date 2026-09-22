using CloudApp.Core.Entities;
using CloudApp.Core.Enums;

namespace CloudApp.Core.Interfaces.Repositories
{
    public interface ISubscriptionRepository : IRepository<UserSubscription>
    {
        /// <summary>
        /// 获取某用户对某目标的订阅信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="targetId"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        Task<UserSubscription?> GetSubscriptionAsync(int userId, int targetId, SubscriptionTargetType targetType, CancellationToken ct = default);

        /// <summary>
        /// 获取某用户的所有订阅信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<List<UserSubscription>> GetAllByUserAsync(int userId, CancellationToken ct = default);

        /// <summary>
        /// 是否已经订阅目标
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="targetId"></param>
        /// <param name="targetType"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<bool> IsSubscribedAsync(int userId, int targetId, SubscriptionTargetType targetType, CancellationToken ct = default);

        /// <summary>
        /// 获取目标的订阅人数
        /// </summary>
        /// <param name="targetId"></param>
        /// <param name="targetType"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<int> GetSubscriberCountAsync(int targetId, SubscriptionTargetType targetType, CancellationToken ct = default);
    }
}
