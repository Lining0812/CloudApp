using CloudApp.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CloudApp.Core.Dtos.Track
{
    /// <summary>
    /// 新增单曲请求
    /// </summary>
    public class CreateTrackRequest
    {
        [Required(ErrorMessage = "单曲名称不能为空")]
        [MaxLength(200, ErrorMessage = "单曲名称不能超过200个字符")]
        public required string Title { get; set; }

        [MaxLength(200, ErrorMessage = "副标题不能超过200个字符")]
        public string? Subtitle { get; set; }

        [MaxLength(1000, ErrorMessage = "描述不能超过1000个字符")]
        public string? Description { get; set; }

        public TimeSpan Duration { get; set; }

        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "原唱不能为空")]
        [MaxLength(100, ErrorMessage = "原唱不能超过100个字符")]
        public required string Artist { get; set; }

        [Required(ErrorMessage = "作曲不能为空")]
        [MaxLength(100, ErrorMessage = "作曲不能超过100个字符")]
        public required string Composer { get; set; }

        [Required(ErrorMessage = "作词不能为空")]
        [MaxLength(100, ErrorMessage = "作词不能超过100个字符")]
        public required string Lyricist { get; set; }

        [MaxLength(500, ErrorMessage = "封面地址不能超过500个字符")]
        public string? CoverUrl { get; set; }

        [MaxLength(500, ErrorMessage = "跳转链接不能超过500个字符")]
        public string? LinkUrl { get; set; }

        public TrackType Type { get; set; } = TrackType.Studio;

        public int? AlbumId { get; set; }
    }
}
