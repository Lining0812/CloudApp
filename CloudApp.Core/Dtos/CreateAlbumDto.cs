using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudApp.Core.Dtos
{
    public class CreateAlbumDto
    {
        [Required(ErrorMessage = "专辑名称不能为空")]
        [MaxLength(50, ErrorMessage = "轨道名称不能超过50个字符")]
        public string Title { get; set; }

        [MaxLength(200, ErrorMessage = "描述不能超过200个字符")]
        public string Description { get; set; }
        [Required(ErrorMessage = "艺术家不能为空")]
        [MaxLength(50, ErrorMessage = "艺术家名称不能超过50个字符")]
        public string Artist { get; set; }
        [Required(ErrorMessage = "发行日期不能为空")]
        public DateTime ReleaseDate { get; set; }
        [MaxLength(200, ErrorMessage = "封面图片URL不能超过200个字符")]
        public string CoverImageUrl { get; set; }
    }
}
