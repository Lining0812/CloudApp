using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CloudApp.Core.Dtos.Concert
{
    public class CreateConcertDto
    {
        [Required(ErrorMessage = "演唱会名不能为空")]
        [MaxLength(50, ErrorMessage = "演唱会名不能超过50个字符")]
        public string Title { get; set; }

        [MaxLength(500, ErrorMessage = "描述不能超过500个字符")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "开始时间不能为空")]
        public DateTime StartAt { get; set; }

        [Required(ErrorMessage = "结束时间不能为空")]
        public DateTime EndAt { get; set; }

        public string Address { get; set; }

        public IFormFile? CoverImage { get; set; }
    }
}
