using CloudApp.Core.Enums;

namespace CloudApp.Core.Entities
{
    /// <summary>
    /// 行程（待完善）
    /// </summary>
    public class Schedule : BaseEntity
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string Artist { get; set; }
        public string? Location { get; set; }
        public DateTime StartTime { get; set; }
        public ScheduleStatus Status { get; set; }
    }
}
