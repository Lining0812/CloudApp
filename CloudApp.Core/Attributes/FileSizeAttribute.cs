using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CloudApp.Core.Attributes
{
    /// <summary>
    /// 文件大小验证特性
    /// </summary>
    public class FileSizeAttribute : ValidationAttribute
    {
        private readonly long _maxSizeInBytes;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="maxSizeInBytes">最大文件大小（字节）</param>
        public FileSizeAttribute(long maxSizeInBytes)
        {
            _maxSizeInBytes = maxSizeInBytes;
        }

        /// <summary>
        /// 验证方法
        /// </summary>
        /// <param name="value">要验证的值</param>
        /// <param name="validationContext">验证上下文</param>
        /// <returns>验证结果</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.Length > _maxSizeInBytes)
                {
                    var maxSizeInMB = _maxSizeInBytes / (1024 * 1024);
                    return new ValidationResult(string.Format(ErrorMessage ?? "文件大小不能超过{0}MB", maxSizeInMB));
                }
            }
            return ValidationResult.Success;
        }
    }
}
