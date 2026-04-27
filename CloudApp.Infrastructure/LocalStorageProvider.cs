using CloudApp.Core.Confige;
using CloudApp.Core.Enums;
using CloudApp.Core.Interfaces;
using CloudApp.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CloudApp.Infrastructure
{
    /// <summary>
    /// 本地文件存储提供者
    /// </summary>
    public class LocalStorageProvider : IStorageProvider
    {
        private readonly ILogger<LocalStorageProvider> _logger;
        private readonly StorageOptions _options;
        private readonly string _basePath;

        public StorageType StorageType => StorageType.Bakeup;

        public LocalStorageProvider(
            IOptions<StorageOptions> options,
            ILogger<LocalStorageProvider> logger)
        {
            _options = options?.Value ?? new StorageOptions();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            // 支持绝对路径或相对路径（相对于当前工作目录）
            var root = _options.LocalStorageRoot;
            if (Path.IsPathRooted(root))
            {
                _basePath = root;
            }
            else
            {
                _basePath = Path.Combine(Directory.GetCurrentDirectory(), root);
            }
        }

        /// <summary>
        /// 存储文件至目录（通过 IFileContent 和实体类型）
        /// </summary>
        public string SaveFile(IFileContent file, Entype entityType)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("文件不能为空", nameof(file));
            }

            ValidateFile(file);

            try
            {
                string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                string fileName = $"{Guid.NewGuid():N}{extension}";
                string relativePath = Path.Combine(entityType.ToString().ToLowerInvariant(), DateTime.UtcNow.ToString("yyyy/MM"), fileName);
                string fullPath = GetFullPath(relativePath);

                string? directory = Path.GetDirectoryName(fullPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                _logger.LogInformation("文件保存成功: {FilePath}", relativePath);
                return relativePath.Replace('\\', '/');
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存文件失败: {FileName}", file.FileName);
                throw;
            }
        }

        /// <summary>
        /// 异步存储文件（通过 IFileContent 和实体类型）
        /// </summary>
        public async Task<string> SaveFileAsync(IFileContent file, Entype entityType, CancellationToken cancellationToken = default)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("文件不能为空", nameof(file));
            }

            ValidateFile(file);

            try
            {
                string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                string fileName = $"{Guid.NewGuid():N}{extension}";
                string relativePath = Path.Combine(entityType.ToString().ToLowerInvariant(), DateTime.UtcNow.ToString("yyyy/MM"), fileName);
                string fullPath = GetFullPath(relativePath);

                string? directory = Path.GetDirectoryName(fullPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.OpenReadStream().CopyToAsync(stream, cancellationToken);
                }

                _logger.LogInformation("文件保存成功: {FilePath}", relativePath);
                return relativePath.Replace('\\', '/');
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存文件失败: {FileName}", file.FileName);
                throw;
            }
        }

        /// <summary>
        /// 存储文件（通过 key 和 stream）
        /// </summary>
        public Uri SaveFile(string key, Stream stream)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("文件键不能为空", nameof(key));
            }

            if (stream == null || stream.Length == 0)
            {
                throw new ArgumentException("文件流不能为空", nameof(stream));
            }

            try
            {
                string fullPath = GetFullPath(key);

                string? directory = Path.GetDirectoryName(fullPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    stream.CopyTo(fileStream);
                }

                _logger.LogInformation("文件保存成功: {Key}", key);

                string relativeUrl = "/" + key.Replace('\\', '/');
                return new Uri(relativeUrl, UriKind.Relative);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存文件失败: {Key}", key);
                throw;
            }
        }

        /// <summary>
        /// 异步存储文件（通过 key 和 stream）
        /// </summary>
        public async Task<Uri> SaveFileAsync(string key, Stream stream, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("文件键不能为空", nameof(key));
            }

            if (stream == null || stream.Length == 0)
            {
                throw new ArgumentException("文件流不能为空", nameof(stream));
            }

            try
            {
                string fullPath = GetFullPath(key);

                string? directory = Path.GetDirectoryName(fullPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await stream.CopyToAsync(fileStream, cancellationToken);
                }

                _logger.LogInformation("文件保存成功: {Key}", key);

                string relativeUrl = "/" + key.Replace('\\', '/');
                return new Uri(relativeUrl, UriKind.Relative);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存文件失败: {Key}", key);
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
        /// 异步读取文件
        /// </summary>
        public Task<Stream> ReadFileAsync(string filepath, CancellationToken cancellationToken = default)
        {
            return Task.Run(() => ReadFile(filepath), cancellationToken);
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
        /// 异步删除文件
        /// </summary>
        public Task DeleteFileAsync(string filepath, CancellationToken cancellationToken = default)
        {
            return Task.Run(() => DeleteFile(filepath), cancellationToken);
        }

        /// <summary>
        /// 更新文件（替换现有文件）
        /// </summary>
        public string UpdateFile(string filepath, IFileContent newFile)
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
                DeleteFile(filepath);

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
        /// 异步更新文件
        /// </summary>
        public async Task<string> UpdateFileAsync(string filepath, IFileContent newFile, CancellationToken cancellationToken = default)
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
                await DeleteFileAsync(filepath, cancellationToken);

                string fullPath = GetFullPath(filepath);
                string? directory = Path.GetDirectoryName(fullPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await newFile.OpenReadStream().CopyToAsync(stream, cancellationToken);
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
        /// 检查文件是否存在
        /// </summary>
        public bool FileExists(string filepath)
        {
            if (string.IsNullOrWhiteSpace(filepath))
                return false;

            try
            {
                string fullPath = GetFullPath(filepath);
                return File.Exists(fullPath);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取文件信息
        /// </summary>
        public FileInfo? GetFileInfo(string filepath)
        {
            if (string.IsNullOrWhiteSpace(filepath))
                return null;

            try
            {
                string fullPath = GetFullPath(filepath);
                if (!File.Exists(fullPath))
                    return null;

                return new FileInfo(fullPath);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 验证文件
        /// </summary>
        public bool ValidateFile(IFileContent file)
        {
            if (file.Length > _options.MaxFileSize)
            {
                throw new ArgumentException($"文件大小超过限制 ({_options.MaxFileSize / 1024 / 1024}MB)", nameof(file));
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (_options.AllowedExtensions != null && _options.AllowedExtensions.Length > 0 && !_options.AllowedExtensions.Contains(extension))
            {
                throw new ArgumentException($"不支持的文件类型: {extension}", nameof(file));
            }
            return true;
        }

        /// <summary>
        /// 获取完整路径
        /// </summary>
        private string GetFullPath(string relativePath)
        {
            if (Path.IsPathRooted(relativePath))
            {
                return relativePath;
            }

            relativePath = relativePath.TrimStart('/', '\\');
            return Path.Combine(_basePath, relativePath);
        }
    }
}
