using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Mime;

namespace CloudApp.Example
{
    public class StreamToIFormFileConverter
    {
        /// <summary>
        /// 将Stream转换为IFormFile
        /// </summary>
        /// <param name="stream">要转换的流</param>
        /// <param name="fileName">文件名</param>
        /// <param name="contentType">内容类型（MIME类型）</param>
        /// <returns>转换后的IFormFile实例</returns>
        public static IFormFile ConvertStreamToIFormFile(Stream stream, string fileName, string contentType = MediaTypeNames.Application.Octet)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            // 确保流的位置在起始位置
            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }

            // 创建FormFile实例
            // 参数说明：
            // 1. stream - 文件内容的流
            // 2. baseStreamOffset - 基础流的起始位置（通常为0）
            // 3. length - 流的长度
            // 4. name - 表单字段名称（可以是任意值）
            // 5. fileName - 原始文件名
            var formFile = new FormFile(
                baseStream: stream,
                baseStreamOffset: 0,
                length: stream.Length,
                name: "file",
                fileName: fileName
            );

            // 设置Content-Type
            formFile.Headers = new HeaderDictionary();
            formFile.Headers.Append("Content-Type", contentType);

            return formFile;
        }

        /// <summary>
        /// 示例：从文件路径创建IFormFile
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>转换后的IFormFile实例</returns>
        public static IFormFile CreateIFormFileFromPath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("文件不存在", filePath);
            }

            // 打开文件流
            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            
            // 获取文件名
            var fileName = Path.GetFileName(filePath);
            
            // 获取内容类型
            var contentType = GetContentType(filePath);
            
            // 转换为IFormFile
            return ConvertStreamToIFormFile(stream, fileName, contentType);
        }

        /// <summary>
        /// 根据文件扩展名获取内容类型
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>内容类型</returns>
        private static string GetContentType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLower();
            return extension switch
            {
                ".jpg" or ".jpeg" => MediaTypeNames.Image.Jpeg,
                ".png" => MediaTypeNames.Image.Png,
                ".gif" => MediaTypeNames.Image.Gif,
                ".pdf" => MediaTypeNames.Application.Pdf,
                ".txt" => MediaTypeNames.Text.Plain,
                _ => MediaTypeNames.Application.Octet
            };
        }
    }
}