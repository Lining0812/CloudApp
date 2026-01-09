using CloudApp.Core.Enums;
using CloudApp.Core.Interfaces.Services;
using CloudApp.Service.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CloudApp.Service
{
    /// <summary>
    /// 本地文件存储提供者
    /// </summary>
    public class LocalStorageProvider : IStorageProvider
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<LocalStorageProvider> _logger;
        private const long MaxFileSize = 10 * 1024 * 1024; // 10MB
        private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".gif", ".mp3", ".mp4", ".pdf"];

        public LocalStorageProvider(IWebHostEnvironment environment, ILogger<LocalStorageProvider> logger)
        {
            _environment = environment;
            _logger = logger;
        }

        /// <summary>
        /// 存储文件至目录
        /// </summary>
        public string SaveFile(IFormFile file, Entype entityType)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("文件不能为空", nameof(file));
            }

            ValidateFile(file);

            try
            {
                string type = entityType.ToString();
                string filePath = FilePathProvider.GenerateFullFilePath(file, entityType);
                string fullPath = GetFullPath(filePath);

                string? directory = Path.GetDirectoryName(fullPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return filePath; // 返回相对路径
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存文件失败: {FileName}", file.FileName);
                throw;
            }
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        public Stream ReadFile(string filepath)
        {
            if (string.IsNullOrWhiteSpace(filepath))
            {
                throw new ArgumentException("文件路径不能为空", nameof(filepath));
            }

            try
            {
                string fullPath = GetFullPath(filepath);
                
                if (!File.Exists(fullPath))
                {
                    throw new FileNotFoundException($"文件不存在: {filepath}");
                }

                return new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "读取文件失败: {FilePath}", filepath);
                throw;
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        public void DeleteFile(string filepath)
        {
            if (string.IsNullOrWhiteSpace(filepath))
            {
                throw new ArgumentException("文件路径不能为空", nameof(filepath));
            }

            try
            {
                string fullPath = GetFullPath(filepath);
                
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    _logger.LogInformation("文件删除成功: {FilePath}", filepath);
                }
                else
                {
                    _logger.LogWarning("要删除的文件不存在: {FilePath}", filepath);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除文件失败: {FilePath}", filepath);
                throw;
            }
        }

        /// <summary>
        /// 更新文件（替换现有文件）
        /// </summary>
        public string UpdateFile(string filepath, IFormFile newFile)
        {
            if (string.IsNullOrWhiteSpace(filepath))
            {
                throw new ArgumentException("文件路径不能为空", nameof(filepath));
            }

            if (newFile == null || newFile.Length == 0)
            {
                throw new ArgumentException("新文件不能为空", nameof(newFile));
            }

            ValidateFile(newFile);

            try
            {
                // 删除旧文件
                DeleteFile(filepath);

                // 保存新文件（使用相同的路径）
                string fullPath = GetFullPath(filepath);
                string? directory = Path.GetDirectoryName(fullPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    newFile.CopyTo(stream);
                }

                _logger.LogInformation("文件更新成功: {FilePath}", filepath);
                return filepath;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新文件失败: {FilePath}", filepath);
                throw;
            }
        }

        /// <summary>
        /// 验证文件
        /// </summary>
        private static void ValidateFile(IFormFile file)
        {
            if (file.Length > MaxFileSize)
            {
                throw new ArgumentException($"文件大小超过限制 ({MaxFileSize / 1024 / 1024}MB)", nameof(file));
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension))
            {
                throw new ArgumentException($"不支持的文件类型: {extension}", nameof(file));
            }
        }

        /// <summary>
        /// 获取完整路径（相对于Web根目录）
        /// </summary>
        private string GetFullPath(string relativePath)
        {
            // 如果已经是绝对路径，直接返回
            if (Path.IsPathRooted(relativePath))
            {
                return relativePath;
            }

            // 处理相对路径，移除开头的斜杠或反斜杠
            relativePath = relativePath.TrimStart('/', '\\');
            
            return Path.Combine(_environment.WebRootPath ?? _environment.ContentRootPath, relativePath);
        }
    }
}
