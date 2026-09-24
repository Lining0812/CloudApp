using CloudApp.Core.Enums;

namespace CloudApp.Core.Entities
{
    /// <summary>
    /// 用户订阅
    /// </summary>
    public class UserSubscription : BaseEntity
    {
        public int UserId { get; set; }
        public int TargetId { get; set; }
        public SubscriptionTargetType TargetType { get; set; }
    }
}
