using CloudApp.Core.Interface;
using Microsoft.AspNetCore.Http;

namespace CloudApp.Service.Services
{
    public class FileService : IFileService
    {
        private readonly string _webRootPath;
        private string[] allowedExtensions = [".jpg", ".jpeg", ".png"];

        /// <summary>
        /// 上传文件方法
        /// </summary>
        /// <param name="file"></param>
        /// <param name="UploadDirectory"></param>
        /// <returns></returns>
        public string UploadFile(IFormFile file, string UploadDirectory)
        {
            ValidateFile(file, allowedExtensions, 5 * 1024 * 1024);

            var fullUploadPath = EnsureUploadPath(UploadDirectory);
            var fileName = GenerateFileName(file);
            var filePath = Path.Combine(fullUploadPath, fileName);

            // 保存文件
            SaveFile(file, filePath);

            return $"/{UploadDirectory}/{fileName}";
        }

        /// <summary>
        /// 验证文件的扩展名和大小
        /// </summary>
        /// <param name="file"></param>
        /// <param name="allowedExtensions"></param>
        /// <param name="maxFileSizeBytes"></param>
        /// <returns></returns>
        public bool ValidateFile(IFormFile file, string[] allowedExtensions, long maxFileSizeBytes)
        {
            if (file == null || file.Length == 0)
            {
                return false;
            }

            else if (file.Length > maxFileSizeBytes)
            {
                return false;
            }

            else
            {
                var fileExtension = Path.GetExtension(file.FileName).ToLower();
                return allowedExtensions.Contains(fileExtension);
            }
        }

        /// <summary>
        /// 创建上传目录
        /// </summary>
        /// <param name="webrootPath"></param>
        /// <param name="uploadDirectory"></param>
        /// <returns></returns>
        public string EnsureUploadPath(string uploadDirectory)
        {
            var fullUploadPath = Path.Combine(_webRootPath, uploadDirectory);
            if (!Directory.Exists(fullUploadPath))
            {
                Directory.CreateDirectory(fullUploadPath);
                return fullUploadPath;
            }
            return fullUploadPath;
        }

        /// <summary>
        /// 生成唯一文件名
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>生成的文件名</returns>
        private string GenerateFileName(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            return $"{Guid.NewGuid()}{extension}";
        }

        /// <summary>
        /// 保存文件到指定路径
        /// </summary>
        /// <param name="file"></param>
        /// <param name="filePath"></param>
        private void SaveFile(IFormFile file, string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }
    }
}
