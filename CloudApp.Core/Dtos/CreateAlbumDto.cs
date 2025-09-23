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
        [Required(ErrorMessage = "专辑名不能为空")]
        [MaxLength(50, ErrorMessage = "专辑名不能超过50个字符")]
        public string Title { get; set; }

        [MaxLength(1000, ErrorMessage = "描述不能超过1000个字符")]
        public string Description { get; set; }

        [Required(ErrorMessage = "艺术家不能为空")]
        [MaxLength(50, ErrorMessage = "艺术家名称不能超过50个字符")]
        public string Artist { get; set; }

        [Required(ErrorMessage = "发行日期不能为空")]
        public DateTime ReleaseDate { get; set; }

        [MaxLength(500, ErrorMessage = "封面图片URL不能超过500个字符")]
        public string CoverImageUrl { get; set; }
    }
}
