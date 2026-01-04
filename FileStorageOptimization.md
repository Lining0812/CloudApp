# 文件存储服务优化方案

## 1. 命名空间组织结构

```
CloudApp.Core
├── Interfaces
│   └── Storage
│       ├── IStorageProvider.cs      # 主存储接口
│       ├── IFileValidator.cs         # 文件验证接口
│       └── IFileOptimizer.cs         # 文件优化接口
├── Models
│   └── Storage
│       ├── StorageSettings.cs        # 存储配置模型
│       └── FileMetadata.cs           # 文件元数据模型
└── Enums
    └── StorageType.cs                # 存储类型枚举

CloudApp.Service
├── Storage
│   ├── Providers
│   │   ├── LocalStorageProvider.cs   # 本地存储实现
│   │   └── AzureBlobStorageProvider.cs # 云存储实现（示例）
│   ├── Validators
│   │   └── FileValidator.cs          # 文件验证实现
│   ├── Optimizers
│   │   └── ImageOptimizer.cs         # 图片优化实现
│   └── StorageFactory.cs             # 存储工厂
└── Extensions
    └── ServiceCollectionExtensions.cs # 服务注册扩展
```

## 2. 核心接口设计

### IStorageProvider.cs
```csharp
using Microsoft.AspNetCore.Http;

namespace CloudApp.Core.Interfaces.Storage
{
    public interface IStorageProvider
    {
        /// <summary>
        /// 保存文件
        /// </summary>
        Task<string> SaveFileAsync(IFormFile file, string folderName, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 保存专辑封面
        /// </summary>
        Task<string> SaveAlbumImageAsync(IFormFile image, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 保存演唱会封面
        /// </summary>
        Task<string> SaveConcertImageAsync(IFormFile image, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 保存单曲封面
        /// </summary>
        Task<string> SaveTrackImageAsync(IFormFile image, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 删除文件
        /// </summary>
        Task<bool> DeleteFileAsync(string filePath, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 更新文件
        /// </summary>
        Task<string> UpdateFileAsync(string oldFilePath, IFormFile newFile, string folderName, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 获取文件URL
        /// </summary>
        string GetFileUrl(string filePath);
    }
}
```

### IFileValidator.cs
```csharp
using Microsoft.AspNetCore.Http;

namespace CloudApp.Core.Interfaces.Storage
{
    public interface IFileValidator
    {
        /// <summary>
        /// 验证文件
        /// </summary>
        bool ValidateFile(IFormFile file, out string errorMessage);
        
        /// <summary>
        /// 验证图片文件
        /// </summary>
        bool ValidateImage(IFormFile image, out string errorMessage);
    }
}
```

### IFileOptimizer.cs
```csharp
using Microsoft.AspNetCore.Http;

namespace CloudApp.Core.Interfaces.Storage
{
    public interface IFileOptimizer
    {
        /// <summary>
        /// 优化图片
        /// </summary>
        Task<Stream> OptimizeImageAsync(IFormFile image, CancellationToken cancellationToken = default);
    }
}
```

## 3. 模型和枚举

### StorageSettings.cs
```csharp
namespace CloudApp.Core.Models.Storage
{
    public class StorageSettings
    {
        /// <summary>
        /// 存储类型
        /// </summary>
        public string StorageType { get; set; } = "Local";
        
        /// <summary>
        /// 本地存储根路径
        /// </summary>
        public string LocalRootPath { get; set; } = "wwwroot";
        
        /// <summary>
        /// 图片存储路径
        /// </summary>
        public string ImagePath { get; set; } = "images";
        
        /// <summary>
        /// 专辑封面路径
        /// </summary>
        public string AlbumImagePath { get; set; } = "albums";
        
        /// <summary>
        /// 演唱会封面路径
        /// </summary>
        public string ConcertImagePath { get; set; } = "concerts";
        
        /// <summary>
        /// 单曲封面路径
        /// </summary>
        public string TrackImagePath { get; set; } = "tracks";
        
        /// <summary>
        /// 默认专辑封面
        /// </summary>
        public string DefaultAlbumImage { get; set; } = "albums/default_cover.jpg";
        
        /// <summary>
        /// 默认演唱会封面
        /// </summary>
        public string DefaultConcertImage { get; set; } = "concerts/default_cover.jpg";
        
        /// <summary>
        /// 默认单曲封面
        /// </summary>
        public string DefaultTrackImage { get; set; } = "tracks/default_cover.jpg";
        
        /// <summary>
        /// 允许的图片类型
        /// </summary>
        public string[] AllowedImageTypes { get; set; } = { ".jpg", ".jpeg", ".png", ".gif" };
        
        /// <summary>
        /// 最大文件大小（字节）
        /// </summary>
        public long MaxFileSize { get; set; } = 10 * 1024 * 1024; // 10MB
    }
}
```

### FileMetadata.cs
```csharp
namespace CloudApp.Core.Models.Storage
{
    public class FileMetadata
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
        
        /// <summary>
        /// 文件大小（字节）
        /// </summary>
        public long FileSize { get; set; }
        
        /// <summary>
        /// 文件类型
        /// </summary>
        public string ContentType { get; set; }
        
        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string Extension { get; set; }
        
        /// <summary>
        /// 存储路径
        /// </summary>
        public string StoragePath { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
```

### StorageType.cs
```csharp
namespace CloudApp.Core.Enums
{
    public enum StorageType
    {
        Local,
        AzureBlobStorage,
        AwsS3
    }
}
```

## 4. 服务实现

### LocalStorageProvider.cs
```csharp
using CloudApp.Core.Interfaces.Storage;
using CloudApp.Core.Models.Storage;
using Microsoft.AspNetCore.Http;

namespace CloudApp.Service.Storage.Providers
{
    public class LocalStorageProvider : IStorageProvider
    {
        private readonly StorageSettings _settings;
        private readonly IFileValidator _fileValidator;
        private readonly IFileOptimizer _fileOptimizer;
        private readonly string _basePath;

        public LocalStorageProvider(
            StorageSettings settings,
            IFileValidator fileValidator,
            IFileOptimizer fileOptimizer)
        {
            _settings = settings;
            _fileValidator = fileValidator;
            _fileOptimizer = fileOptimizer;
            _basePath = Path.Combine(Directory.GetCurrentDirectory(), _settings.LocalRootPath);

            // 确保目录存在
            InitializeDirectories();
        }

        private void InitializeDirectories()
        {
            var directories = new List<string>
            {
                Path.Combine(_basePath, _settings.ImagePath, _settings.AlbumImagePath),
                Path.Combine(_basePath, _settings.ImagePath, _settings.ConcertImagePath),
                Path.Combine(_basePath, _settings.ImagePath, _settings.TrackImagePath)
            };

            foreach (var directory in directories)
            {
                Directory.CreateDirectory(directory);
            }
        }

        public async Task<string> SaveFileAsync(IFormFile file, string folderName, CancellationToken cancellationToken = default)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentNullException(nameof(file), "文件不能为空");

            // 验证文件
            if (!_fileValidator.ValidateFile(file, out var errorMessage))
                throw new InvalidOperationException(errorMessage);

            // 生成唯一文件名
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var folderPath = Path.Combine(_basePath, folderName);
            var filePath = Path.Combine(folderPath, fileName);

            // 确保文件夹存在
            Directory.CreateDirectory(folderPath);

            // 优化并保存文件
            using (var optimizedStream = await _fileOptimizer.OptimizeImageAsync(file, cancellationToken))
            {
                optimizedStream.Position = 0;
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await optimizedStream.CopyToAsync(fileStream, cancellationToken);
                }
            }

            return $"{folderName}/{fileName}";
        }

        public async Task<string> SaveAlbumImageAsync(IFormFile image, CancellationToken cancellationToken = default)
        {
            if (image == null)
                return _settings.DefaultAlbumImage;

            var folderPath = Path.Combine(_settings.ImagePath, _settings.AlbumImagePath);
            return await SaveFileAsync(image, folderPath, cancellationToken);
        }

        public async Task<string> SaveConcertImageAsync(IFormFile image, CancellationToken cancellationToken = default)
        {
            if (image == null)
                return _settings.DefaultConcertImage;

            var folderPath = Path.Combine(_settings.ImagePath, _settings.ConcertImagePath);
            return await SaveFileAsync(image, folderPath, cancellationToken);
        }

        public async Task<string> SaveTrackImageAsync(IFormFile image, CancellationToken cancellationToken = default)
        {
            if (image == null)
                return _settings.DefaultTrackImage;

            var folderPath = Path.Combine(_settings.ImagePath, _settings.TrackImagePath);
            return await SaveFileAsync(image, folderPath, cancellationToken);
        }

        public async Task<bool> DeleteFileAsync(string filePath, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(filePath))
                return false;

            var fullPath = Path.Combine(_basePath, filePath);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }

            return false;
        }

        public async Task<string> UpdateFileAsync(string oldFilePath, IFormFile newFile, string folderName, CancellationToken cancellationToken = default)
        {
            // 删除旧文件
            await DeleteFileAsync(oldFilePath, cancellationToken);
            
            // 保存新文件
            return await SaveFileAsync(newFile, folderName, cancellationToken);
        }

        public string GetFileUrl(string filePath)
        {
            return $"/{filePath}";
        }
    }
}
```

### FileValidator.cs
```csharp
using CloudApp.Core.Interfaces.Storage;
using CloudApp.Core.Models.Storage;
using Microsoft.AspNetCore.Http;

namespace CloudApp.Service.Storage.Validators
{
    public class FileValidator : IFileValidator
    {
        private readonly StorageSettings _settings;

        public FileValidator(StorageSettings settings)
        {
            _settings = settings;
        }

        public bool ValidateFile(IFormFile file, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (file == null || file.Length == 0)
            {
                errorMessage = "文件不能为空";
                return false;
            }

            if (file.Length > _settings.MaxFileSize)
            {
                errorMessage = $"文件大小不能超过 {_settings.MaxFileSize / (1024 * 1024)}MB";
                return false;
            }

            return true;
        }

        public bool ValidateImage(IFormFile image, out string errorMessage)
        {
            if (!ValidateFile(image, out errorMessage))
                return false;

            var extension = Path.GetExtension(image.FileName).ToLower();
            if (!_settings.AllowedImageTypes.Contains(extension))
            {
                errorMessage = $"不支持的图片类型，仅允许：{string.Join(", ", _settings.AllowedImageTypes)}";
                return false;
            }

            // 验证文件头，防止恶意文件
            using (var stream = image.OpenReadStream())
            {
                var buffer = new byte[8];
                stream.Read(buffer, 0, 8);
                
                // 简单的图片文件头验证
                var fileHeaders = new Dictionary<string, byte[]> {
                    {".jpg", new byte[] { 0xFF, 0xD8 }},
                    {".png", new byte[] { 0x89, 0x50, 0x4E, 0x47 }},
                    {".gif", new byte[] { 0x47, 0x49, 0x46 }}
                };

                if (!fileHeaders.ContainsKey(extension))
                    return true;

                var header = fileHeaders[extension];
                return buffer.Take(header.Length).SequenceEqual(header);
            }
        }
    }
}
```

### ImageOptimizer.cs
```csharp
using CloudApp.Core.Interfaces.Storage;
using Microsoft.AspNetCore.Http;

namespace CloudApp.Service.Storage.Optimizers
{
    public class ImageOptimizer : IFileOptimizer
    {
        public async Task<Stream> OptimizeImageAsync(IFormFile image, CancellationToken cancellationToken = default)
        {
            // 检查是否为图片文件
            var extension = Path.GetExtension(image.FileName).ToLower();
            var isImage = new[] {".jpg", ".jpeg", ".png"}.Contains(extension);
            
            if (!isImage)
            {
                // 非图片文件直接返回原始流
                var stream = new MemoryStream();
                await image.CopyToAsync(stream, cancellationToken);
                stream.Position = 0;
                return stream;
            }

            // 这里可以添加图片优化逻辑，比如压缩、调整大小等
            // 示例：直接返回原始流，实际项目中可以使用 ImageSharp 等库进行优化
            var resultStream = new MemoryStream();
            await image.CopyToAsync(resultStream, cancellationToken);
            resultStream.Position = 0;
            return resultStream;
        }
    }
}
```

### StorageFactory.cs
```csharp
using CloudApp.Core.Interfaces.Storage;
using CloudApp.Core.Models.Storage;
using CloudApp.Service.Storage.Providers;

namespace CloudApp.Service.Storage
{
    public class StorageFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public StorageFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IStorageProvider CreateStorageProvider(StorageSettings settings)
        {
            return settings.StorageType.ToLower() switch
            {
                "local" => _serviceProvider.GetRequiredService<LocalStorageProvider>(),
                // 可以添加其他存储类型的实现
                // "azureblobstorage" => _serviceProvider.GetRequiredService<AzureBlobStorageProvider>(),
                _ => throw new ArgumentException($"不支持的存储类型: {settings.StorageType}")
            };
        }
    }
}
```

## 5. 服务注册

### ServiceCollectionExtensions.cs
```csharp
using CloudApp.Core.Interfaces.Storage;
using CloudApp.Core.Models.Storage;
using CloudApp.Service.Storage;
using CloudApp.Service.Storage.Optimizers;
using CloudApp.Service.Storage.Providers;
using CloudApp.Service.Storage.Validators;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StorageServiceCollectionExtensions
    {
        public static IServiceCollection AddStorageServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 注册配置
            services.Configure<StorageSettings>(configuration.GetSection("Storage"));
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<StorageSettings>>().Value);

            // 注册存储服务
            services.AddSingleton<IFileValidator, FileValidator>();
            services.AddSingleton<IFileOptimizer, ImageOptimizer>();
            services.AddSingleton<LocalStorageProvider>();
            services.AddSingleton<StorageFactory>();
            
            // 根据配置注册主存储服务
            services.AddSingleton<IStorageProvider>(sp =>
            {
                var settings = sp.GetRequiredService<StorageSettings>();
                var factory = sp.GetRequiredService<StorageFactory>();
                return factory.CreateStorageProvider(settings);
            });

            return services;
        }
    }
}
```

## 6. 配置示例

### appsettings.json
```json
{
  "Storage": {
    "StorageType": "Local",
    "LocalRootPath": "wwwroot",
    "ImagePath": "images",
    "AlbumImagePath": "albums",
    "ConcertImagePath": "concerts",
    "TrackImagePath": "tracks",
    "DefaultAlbumImage": "albums/default_cover.jpg",
    "DefaultConcertImage": "concerts/default_cover.jpg",
    "DefaultTrackImage": "tracks/default_cover.jpg",
    "AllowedImageTypes": [".jpg", ".jpeg", ".png", ".gif"],
    "MaxFileSize": 10485760
  }
}
```

## 7. 使用示例

### 在控制器中使用
```csharp
using CloudApp.Core.Interfaces.Storage;
using Microsoft.AspNetCore.Mvc;

namespace CloudApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IStorageProvider _storageProvider;

        public FileController(IStorageProvider storageProvider)
        {
            _storageProvider = storageProvider;
        }

        [HttpPost("upload-album-image")]
        public async Task<IActionResult> UploadAlbumImage(IFormFile image)
        {
            var imageUrl = await _storageProvider.SaveAlbumImageAsync(image);
            return Ok(new { imageUrl });
        }
    }
}
```

## 8. 优化亮点

1. **解耦设计**：通过接口抽象，实现了存储服务与具体实现的解耦
2. **可扩展性**：支持多种存储类型，方便未来扩展到云存储
3. **配置驱动**：存储路径和设置通过配置文件管理，更加灵活
4. **完善的验证机制**：实现了文件类型、大小、安全等多维度验证
5. **文件优化支持**：预留了图片优化接口，可集成图片压缩等功能
6. **异步设计**：所有IO操作均采用异步方式，提高系统吞吐量
7. **清晰的命名空间**：模块化的命名空间设计，提高代码的可维护性
8. **完善的错误处理**：提供了详细的错误信息，便于调试和用户反馈

## 9. 迁移建议

1. **逐步替换**：先在新项目中使用优化后的存储服务，然后逐步迁移现有功能
2. **保持兼容性**：可以为旧接口提供适配器，确保平滑迁移
3. **数据迁移**：如果切换存储类型，需要编写数据迁移工具，将现有文件迁移到新的存储系统
4. **测试覆盖**：为存储服务编写完善的单元测试和集成测试，确保功能正确性
5. **监控和日志**：添加监控和日志功能，便于追踪和排查存储相关问题
