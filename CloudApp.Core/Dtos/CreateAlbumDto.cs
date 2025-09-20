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
    }
}
