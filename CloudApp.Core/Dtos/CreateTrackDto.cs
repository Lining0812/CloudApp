using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudApp.Core.Dtos
{
    public class CreateTrackDto
    {
        [Required(ErrorMessage = "专辑名称不能为空")]
        [MaxLength(200, ErrorMessage = "歌名不能超过200个字符")]
        public string Title { get; set; }

        [MaxLength(200, ErrorMessage = "副标题不能超过200个字符")]
        public string? Subtitle { get; set; } = string.Empty;

        [MaxLength(1000, ErrorMessage = "描述不能超过1000个字符")]
        public string? Description { get; set; } = string.Empty;

        [Required(ErrorMessage ="原唱不能为空")]
        [MaxLength(100, ErrorMessage = "轨道名称不能超过100个字符")]
        public string Artist { get; set; }

        [Required(ErrorMessage = "作曲不能为空")]
        [MaxLength(100, ErrorMessage = "作曲者不能超过100个字符")]
        public string Composer { get; set; }

        [Required(ErrorMessage = "作词不能为空")]
        [MaxLength(100, ErrorMessage = "作词者不能超过100个字符")]
        public string Lyricist { get; set; }

        public string URL { get; set; }

        [Required(ErrorMessage = "所属专辑ID不能为空")]
        public int AlbumId { get; set; }
    }
}
