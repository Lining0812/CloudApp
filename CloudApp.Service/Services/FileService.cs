using CloudApp.Core.Interface;
using Microsoft.AspNetCore.Http;

namespace CloudApp.Service.Services
{
    public class FileService : IFileService
    {
        private readonly string _webRootPath;
        private string[] allowedExtensions = [".jpg", ".jpeg", ".png"];

        public FileService(string webRootPath)
        {
            _webRootPath = webRootPath;
        }

        public string UploadFile(IFormFile file, string UploadDirectory)
        {
            ValidateFile(file, allowedExtensions, 5 * 1024 * 1024);

            var fullUploadPath = EnsureUploadPath(UploadDirectory);
            var fileName = GenerateFileName(file);
            var filePath = Path.Combine(fullUploadPath, fileName);

            SaveFile(file, filePath);

            return $"/{UploadDirectory}/{fileName}";
        }
        public bool ValidateFile(IFormFile file, string[] allowedExtensions, long maxFileSizeBytes)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("文件为空");
            }

            else if (file.Length > maxFileSizeBytes)
            {
                throw new ArgumentException($"文件大小超过限制({maxFileSizeBytes / 1024 / 1024}MB)");
            }

            else
            {
                var fileExtension = Path.GetExtension(file.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    throw new ArgumentException($"不支持的文件类型，仅支持: {string.Join(',', allowedExtensions)}");
                }
                return true;
            }
        }
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
        public string GenerateFileName(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            return $"{Guid.NewGuid()}{extension}";
        }
        public void SaveFile(IFormFile file, string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }
    }
}