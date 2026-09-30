using CloudApp.Core.Enums;

namespace CloudApp.Core.Interfaces.Services
{
    public interface ITargetValidator
    {
        /// <summary>
        /// 验证目标是否存在
        /// </summary>
        /// <param name="targetId"></param>
        /// <param name="targetType"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<bool> TargetExistsAsync(int targetId, SubscriptionTargetType targetType, CancellationToken ct = default);
    }
}
