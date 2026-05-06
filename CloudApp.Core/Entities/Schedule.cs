using CloudApp.Core.Enums;

namespace CloudApp.Core.Entities
{
    /// <summary>
    /// 行程
    /// </summary>
    public class Schedule : BaseEntity
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string Artist { get; set; }
        public string? Location { get; set; }
        public DateOnly StartTime { get; set; }
        public DateOnly EndTime { get; set; }
        public ScheduleType Type { get; set; }
        public ScheduleStatus Status { get; set; }
        public bool IsPublic { get; set; }
    }
}
