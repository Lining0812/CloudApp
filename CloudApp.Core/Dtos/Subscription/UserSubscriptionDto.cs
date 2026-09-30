using CloudApp.Core.Enums;

namespace CloudApp.Core.Dtos.Subscription
{
    /// <summary>
    /// 用户订阅信息（含目标详情）
    /// </summary>
    public class UserSubscriptionDto
    {
        /// <summary>
        /// 订阅记录Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 订阅目标Id
        /// </summary>
        public int TargetId { get; set; }

        /// <summary>
        /// 订阅目标类型
        /// </summary>
        public SubscriptionTargetType TargetType { get; set; }

        /// <summary>
        /// 目标标题（如演唱会标题）；目标已删除或类型暂不支持时为空
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 目标描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 目标开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 目标结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 目标地点（仅演唱会有值）
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// 目标封面图（仅演唱会有值）
        /// </summary>
        public string? CoverUrl { get; set; }
    }
}
