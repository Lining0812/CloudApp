using CloudApp.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace CloudApp.Service.Utils
{
    /// <summary>
    /// 文件路径生成工具类
    /// </summary>
    public static class FilePathProvider
    {
        /// <summary>
        /// 实体类型到目录名称的映射
        /// </summary>
        private static readonly Dictionary<Entype, string> TypeToDirectoryMapping = new()
        {
            { Entype.Album, "albums" },
            { Entype.Concert, "concerts" },
            { Entype.Track, "tracks" }
        };

        /// <summary>
        /// 默认根目录名称
        /// </summary>
        private const string DefaultRootPath = "uploads";

        /// <summary>
        /// 生成唯一文件名（GUID + 原扩展名）
        /// </summary>
        /// <param name="originalFileName">原始文件名</param>
        /// <returns>唯一文件名</returns>
        private static string GenerateUniqueFileName(string originalFileName)
        {
            if (string.IsNullOrWhiteSpace(originalFileName))
            {
                throw new ArgumentNullException(nameof(originalFileName), "原始文件名不能为空");
            }

            var extension = Path.GetExtension(originalFileName);
            if (string.IsNullOrEmpty(extension))
            {
                extension = ".tmp"; // 如果没有扩展名，使用默认扩展名
            }

            var uniqueName = $"{Guid.NewGuid()}{extension}";
            return uniqueName;
        }

        /// <summary>
        /// 根据实体类型获取目录名称
        /// </summary>
        /// <param name="entityType">实体类型枚举</param>
        /// <returns>目录名称</returns>
        private static string GetDirectoryName(Entype entityType)
        {
            // 从映射中获取目录名，如果不存在则使用枚举名称（转小写）
            return TypeToDirectoryMapping.TryGetValue(entityType, out var directoryName)
                ? directoryName
                : entityType.ToString().ToLowerInvariant();
        }

        /// <summary>
        /// 生成完整的文件存储相对路径
        /// </summary>
        /// <param name="originalFileName">原始文件名</param>
        /// <param name="entityType">实体类型枚举</param>
        /// <returns>文件相对路径（如 "uploads/albums/{guid}.jpg"）</returns>
        public static string GenerateFullFilePath(string originalFileName, Entype entityType)
        {
            if (string.IsNullOrWhiteSpace(originalFileName))
            {
                throw new ArgumentNullException(nameof(originalFileName), "原始文件名不能为空");
            }

            var uniqueFileName = GenerateUniqueFileName(originalFileName);
            var directoryName = GetDirectoryName(entityType);
            
            // 生成相对路径：uploads/{目录名}/{唯一文件名}
            var relativePath = Path.Combine(DefaultRootPath, directoryName, uniqueFileName)
                .Replace('\\', '/'); // 统一使用正斜杠，便于跨平台和URL使用

            return relativePath;
        }

        /// <summary>
        /// 生成完整的文件存储相对路径（从 IFormFile）
        /// </summary>
        /// <param name="file">上传的文件</param>
        /// <param name="entityType">实体类型枚举</param>
        /// <returns>文件相对路径</returns>
        public static string GenerateFullFilePath(IFormFile file, Entype entityType)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file), "文件不能为空");
            }

            if (string.IsNullOrWhiteSpace(file.FileName))
            {
                throw new ArgumentException("文件名不能为空", nameof(file));
            }

            return GenerateFullFilePath(file.FileName, entityType);
        }
    }
}
