using System.ComponentModel.DataAnnotations;

namespace CloudApp.Core.Dtos.Concert
{
    public class CreateConcertRequest
    {
        [Required(ErrorMessage = "演唱会名不能为空")]
        [MaxLength(50, ErrorMessage = "演唱会名不能超过50个字符")]
        public required string Title { get; set; }

        [MaxLength(500, ErrorMessage = "描述不能超过500个字符")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "开始时间不能为空")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "结束时间不能为空")]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "演唱会地址不能为空")]
        public required string Location { get; set; }

        public string? CoverUrl { get; set; }
    }
}
