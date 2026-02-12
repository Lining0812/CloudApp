using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CloudApp.Core.Dtos.Track
{
    public class CreateTrackDto
    {
        [Required(ErrorMessage = "单曲名称不能为空")]
        [MaxLength(200, ErrorMessage = "单曲名称不能超过200个字符")]
        public string Title { get; set; }

        [MaxLength(200, ErrorMessage = "副标题不能超过200个字符")]
        public string? Subtitle { get; set; } = string.Empty;

        [MaxLength(1000, ErrorMessage = "描述不能超过1000个字符")]
        public string? Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "时长不能为空")]
        public TimeSpan Duration { get; set; }

        [Required(ErrorMessage = "发行时间不能为空")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage ="原唱不能为空")]
        [MaxLength(100, ErrorMessage = "原唱不能超过100个字符")]
        public string Artist { get; set; }

        [Required(ErrorMessage = "作曲不能为空")]
        [MaxLength(100, ErrorMessage = "作曲不能超过100个字符")]
        public string Composer { get; set; }

        [Required(ErrorMessage = "作词不能为空")]
        [MaxLength(100, ErrorMessage = "作词不能超过100个字符")]
        public string Lyricist { get; set; }

        public IFormFile CoverImage { get; set; }

        public int? AlbumId { get; set; }
        public int? ConcertId { get; set; }
    }
}
