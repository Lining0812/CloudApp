using CloudApp.Core.Confige;
using CloudApp.Core.Entities;
using CloudApp.Core.Enums;
using CloudApp.Core.Interfaces.Repositories;
using CloudApp.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace CloudApp.Application
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;

        private readonly IStorageProvider _backupStorage;
        private readonly IStorageProvider? _remoteStorage;
        private readonly ILogger<FileService> _logger;
        private readonly StorageOptions _options;

        public FileService(
            IFileRepository fileRepository,
            IEnumerable<IStorageProvider> storages,
            IOptions<StorageOptions> options,
            ILogger<FileService> logger)
        {
            _fileRepository = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _options = options?.Value ?? new StorageOptions();

            var storageList = storages?.ToList() ?? new List<IStorageProvider>();
            _backupStorage = storageList.FirstOrDefault(s => s.StorageType == StorageType.Bakeup)
                ?? throw new InvalidOperationException("未找到备份存储提供者");
            _remoteStorage = storageList.FirstOrDefault(s => s.StorageType == StorageType.Public);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        public async Task<UploadedFile> UploadAsync(Stream stream, string fileName, CancellationToken cancellationToken = default)
        {
            if (stream == null || stream.Length == 0)
            {
                throw new ArgumentException("文件流不能为空", nameof(stream));
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("文件名不能为空", nameof(fileName));
            }

            try
            {
                // 计算文件 SHA256 哈希
                string hash = await ComputeSha256HashAsync(stream, cancellationToken);
                long fileSize = stream.Length;
                DateTime today = DateTime.Today;

                string key = $"{_options.LocalStorageRoot}/{today:yyyy/MM/dd}/{hash}/{fileName}";

                // 检查是否已存在相同文件（去重）
                if (_options.EnableDeduplication)
                {
                    var oldFile = await _fileRepository.FindFileAsync(fileSize, hash);
                    if (oldFile != null)
                    {
                        _logger.LogInformation("文件已存在，直接返回已有记录: {FileName}, Hash={Hash}", fileName, hash);
                        return oldFile;
                    }
                }

                // 保存到备份存储
                stream.Position = 0;
                Uri backupUrl = await _backupStorage.SaveFileAsync(key, stream, cancellationToken);

                // 保存到远程/公共存储（如果配置了）
                Uri? remoteUrl = null;
                if (_remoteStorage != null)
                {
                    stream.Position = 0;
                    remoteUrl = await _remoteStorage.SaveFileAsync(key, stream, cancellationToken);
                }

                // 创建并持久化记录
                var uploadedFile = UploadedFile.Create(fileName, fileSize, hash, backupUrl, remoteUrl);
                await _fileRepository.AddAsync(uploadedFile);
                await _fileRepository.SaveChangeAsync();

                _logger.LogInformation("文件上传成功: {FileName}, Size={FileSize}, Hash={Hash}", fileName, fileSize, hash);
                return uploadedFile;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "文件上传失败: {FileName}", fileName);
                throw;
            }
        }

        /// <summary>
        /// 根据ID下载文件
        /// </summary>
        public async Task<(Stream Stream, string FileName)> DownloadAsync(int id, CancellationToken cancellationToken = default)
        {
            var file = await _fileRepository.GetByIdAsync(id);
            if (file == null)
            {
                throw new FileNotFoundException($"文件记录不存在: {id}");
            }

            // 优先从备份存储读取
            string? filePath = file.BackUpUrl?.OriginalString ?? file.RemoteUrl?.OriginalString;
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new InvalidOperationException($"文件路径为空: {id}");
            }

            var stream = await _backupStorage.ReadFileAsync(filePath, cancellationToken);
            return (stream, file.FileName);
        }

        /// <summary>
        /// 根据ID获取文件信息
        /// </summary>
        public Task<UploadedFile?> GetFileInfoAsync(int id, CancellationToken cancellationToken = default)
        {
            return _fileRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// 删除文件（从存储和数据库）
        /// </summary>
        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var file = await _fileRepository.GetByIdAsync(id);
            if (file == null)
            {
                _logger.LogWarning("要删除的文件记录不存在: {Id}", id);
                return;
            }

            try
            {
                // 删除备份存储中的文件
                if (file.BackUpUrl != null && !string.IsNullOrWhiteSpace(file.BackUpUrl.OriginalString))
                {
                    await _backupStorage.DeleteFileAsync(file.BackUpUrl.OriginalString, cancellationToken);
                }

                // 删除远程存储中的文件（如果存在）
                if (_remoteStorage != null && file.RemoteUrl != null && !string.IsNullOrWhiteSpace(file.RemoteUrl.OriginalString))
                {
                    await _remoteStorage.DeleteFileAsync(file.RemoteUrl.OriginalString, cancellationToken);
                }

                // 删除数据库记录
                await _fileRepository.DeleteAsync(id);
                await _fileRepository.SaveChangeAsync();

                _logger.LogInformation("文件删除成功: {Id}, {FileName}", id, file.FileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "文件删除失败: {Id}", id);
                throw;
            }
        }

        /// <summary>
        /// 搜索文件
        /// </summary>
        public Task<IEnumerable<UploadedFile>> SearchAsync(string fileName, int skip = 0, int take = 20, CancellationToken cancellationToken = default)
        {
            return _fileRepository.SearchByFileNameAsync(fileName, skip, take);
        }

        /// <summary>
        /// 分页获取文件列表
        /// </summary>
        public Task<IEnumerable<UploadedFile>> GetPagedAsync(int skip = 0, int take = 20, CancellationToken cancellationToken = default)
        {
            return _fileRepository.GetPagedAsync(skip, take);
        }

        /// <summary>
        /// 获取文件总数
        /// </summary>
        public Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return _fileRepository.CountAsync();
        }

        /// <summary>
        /// 计算流的 SHA256 哈希值
        /// </summary>
        private static async Task<string> ComputeSha256HashAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            stream.Position = 0;
            using var sha256 = SHA256.Create();
            byte[] hashBytes = await sha256.ComputeHashAsync(stream, cancellationToken);
            return Convert.ToHexString(hashBytes);
        }
    }
}
