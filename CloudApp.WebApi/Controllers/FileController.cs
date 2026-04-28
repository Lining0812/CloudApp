using CloudApp.Core.Confige;
using CloudApp.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CloudApp.WebApi.Controllers
{
    /// <summary>
    /// 文件管理控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IStorageProvider _storageProvider;
        private readonly StorageOptions _options;
        private readonly ILogger<FileController> _logger;

        public FileController(
            IFileService fileService,
            IStorageProvider storageProvider,
            IOptions<StorageOptions> options,
            ILogger<FileController> logger)
        {
            _fileService = fileService;
            _storageProvider = storageProvider;
            _options = options.Value;
            _logger = logger;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file">上传的文件</param>
        /// <returns>上传后的文件信息</returns>
        [HttpPost("upload")]
        [RequestSizeLimit(100 * 1024 * 1024)] // 100MB 请求大小限制
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { message = "文件不能为空" });
            }

            if (file.Length > _options.MaxFileSize)
            {
                return BadRequest(new { message = $"文件大小超过限制 ({_options.MaxFileSize / 1024 / 1024}MB)" });
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (_options.AllowedExtensions != null && _options.AllowedExtensions.Length > 0 && !_options.AllowedExtensions.Contains(extension))
            {
                return BadRequest(new { message = $"不支持的文件类型: {extension}" });
            }

            try
            {
                await using var stream = file.OpenReadStream();
                var uploadedFile = await _fileService.UploadAsync(stream, file.FileName);
                return Ok(new
                {
                    id = uploadedFile.Id,
                    fileName = uploadedFile.FileName,
                    fileSize = uploadedFile.FileSize,
                    hash = uploadedFile.FileSHA256Hash,
                    backupUrl = uploadedFile.BackUpUrl?.ToString(),
                    remoteUrl = uploadedFile.RemoteUrl?.ToString(),
                    createdAt = uploadedFile.CreatedAt
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "文件上传失败: {FileName}", file.FileName);
                return StatusCode(500, new { message = "文件上传失败", detail = ex.Message });
            }
        }

        /// <summary>
        /// 批量上传文件
        /// </summary>
        /// <param name="files">上传的文件列表</param>
        /// <returns>上传后的文件信息列表</returns>
        [HttpPost("upload/batch")]
        [RequestSizeLimit(500 * 1024 * 1024)] // 500MB 请求大小限制
        public async Task<IActionResult> UploadBatch(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest(new { message = "文件列表不能为空" });
            }

            var results = new List<object>();
            var errors = new List<object>();

            foreach (var file in files)
            {
                if (file.Length > _options.MaxFileSize)
                {
                    errors.Add(new { fileName = file.FileName, error = $"文件大小超过限制 ({_options.MaxFileSize / 1024 / 1024}MB)" });
                    continue;
                }

                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (_options.AllowedExtensions != null && _options.AllowedExtensions.Length > 0 && !_options.AllowedExtensions.Contains(extension))
                {
                    errors.Add(new { fileName = file.FileName, error = $"不支持的文件类型: {extension}" });
                    continue;
                }

                try
                {
                    await using var stream = file.OpenReadStream();
                    var uploadedFile = await _fileService.UploadAsync(stream, file.FileName);
                    results.Add(new
                    {
                        id = uploadedFile.Id,
                        fileName = uploadedFile.FileName,
                        fileSize = uploadedFile.FileSize,
                        hash = uploadedFile.FileSHA256Hash,
                        backupUrl = uploadedFile.BackUpUrl?.ToString(),
                        remoteUrl = uploadedFile.RemoteUrl?.ToString(),
                        createdAt = uploadedFile.CreatedAt
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "文件上传失败: {FileName}", file.FileName);
                    errors.Add(new { fileName = file.FileName, error = ex.Message });
                }
            }

            return Ok(new { succeeded = results, failed = errors });
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="id">文件ID</param>
        /// <returns>文件流</returns>
        [HttpGet("download/{id:int}")]
        public async Task<IActionResult> Download(int id)
        {
            try
            {
                var (stream, fileName) = await _fileService.DownloadAsync(id);
                return File(stream, "application/octet-stream", fileName);
            }
            catch (FileNotFoundException)
            {
                return NotFound(new { message = $"文件不存在: {id}" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "文件下载失败: {Id}", id);
                return StatusCode(500, new { message = "文件下载失败", detail = ex.Message });
            }
        }

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="id">文件ID</param>
        /// <returns>文件信息</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFileInfo(int id)
        {
            var file = await _fileService.GetFileInfoAsync(id);
            if (file == null)
            {
                return NotFound(new { message = $"文件不存在: {id}" });
            }

            return Ok(new
            {
                id = file.Id,
                fileName = file.FileName,
                fileSize = file.FileSize,
                hash = file.FileSHA256Hash,
                backupUrl = file.BackUpUrl?.ToString(),
                remoteUrl = file.RemoteUrl?.ToString(),
                createdAt = file.CreatedAt,
                updatedAt = file.UpdatedAt
            });
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="id">文件ID</param>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _fileService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "文件删除失败: {Id}", id);
                return StatusCode(500, new { message = "文件删除失败", detail = ex.Message });
            }
        }

        /// <summary>
        /// 搜索文件
        /// </summary>
        /// <param name="fileName">文件名关键词</param>
        /// <param name="skip">跳过数量</param>
        /// <param name="take">获取数量</param>
        /// <returns>文件列表</returns>
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string fileName, [FromQuery] int skip = 0, [FromQuery] int take = 20)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return BadRequest(new { message = "搜索关键词不能为空" });
            }

            var files = await _fileService.SearchAsync(fileName, skip, take);
            return Ok(files.Select(f => new
            {
                id = f.Id,
                fileName = f.FileName,
                fileSize = f.FileSize,
                hash = f.FileSHA256Hash,
                backupUrl = f.BackUpUrl?.ToString(),
                remoteUrl = f.RemoteUrl?.ToString(),
                createdAt = f.CreatedAt
            }));
        }

        /// <summary>
        /// 分页获取文件列表
        /// </summary>
        /// <param name="skip">跳过数量</param>
        /// <param name="take">获取数量</param>
        /// <returns>文件列表</returns>
        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] int skip = 0, [FromQuery] int take = 20)
        {
            var files = await _fileService.GetPagedAsync(skip, take);
            var total = await _fileService.CountAsync();

            return Ok(new
            {
                total,
                skip,
                take,
                items = files.Select(f => new
                {
                    id = f.Id,
                    fileName = f.FileName,
                    fileSize = f.FileSize,
                    hash = f.FileSHA256Hash,
                    backupUrl = f.BackUpUrl?.ToString(),
                    remoteUrl = f.RemoteUrl?.ToString(),
                    createdAt = f.CreatedAt
                })
            });
        }
    }
}
