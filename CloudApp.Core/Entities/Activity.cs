using CloudApp.Core.Enums;

namespace CloudApp.Core.Entities
{
    public abstract class Activity : BaseEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        public required string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public ActivityStatus Status { get; set; } = ActivityStatus.Draft;
        /// <summary>
        /// 是否公开
        /// </summary>
        public bool IsPublic { get; set; } = true;
    }
}
