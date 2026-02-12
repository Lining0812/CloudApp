using CloudApp.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CloudApp.Core.Dtos.Media
{
    public class CreateMeidaDto
    {
        /// <summary>
        /// 文件名
        /// </summary>
        [Required(ErrorMessage = "文件名不能为空")]
        [MaxLength(100, ErrorMessage = "文件名不能超过100个字符")]
        public string FileName { get; set; } = string.Empty;
        /// <summary>
        /// 文件路径
        /// </summary>
        [Required(ErrorMessage = "文件路径不能为空")]
        [MaxLength(200, ErrorMessage = "文件路径不能超过200个字符")]
        public string FilePath { get; set; } = string.Empty;
        /// <summary>
        /// 文件 MIME 类型
        /// </summary>
        [Required(ErrorMessage = "MIME类型不能为空")]
        [MaxLength(50, ErrorMessage = "MIME类型不能超过50个字符")]
        public string ContentType { get; set; } = string.Empty;
        /// <summary>
        /// 资源类型
        /// </summary>
        public MediaType MediaType { get; set; }
    }
}
