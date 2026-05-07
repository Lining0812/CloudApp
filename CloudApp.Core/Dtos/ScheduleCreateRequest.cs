using CloudApp.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CloudApp.Core.Dtos
{
    public class ScheduleCreateRequest
    {
        [Required(ErrorMessage = "Title is required.")]
        public required string Title { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Artist is required.")]
        public required string Artist { get; set; }
        public string? Location { get; set; }
        public DateOnly StartTime { get; set; }
        public DateOnly Endtime { get; set; }
        public ScheduleType Type { get; set; } = ScheduleType.Other;
        public ScheduleStatus Status { get; set; } = ScheduleStatus.Scheduled;
        public bool IsPublic { get; set; } = true;
    }
}
