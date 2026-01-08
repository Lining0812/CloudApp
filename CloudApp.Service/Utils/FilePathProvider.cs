
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CloudApp.Service.Utils
{
    /// <summary>
    /// 文件路径生成
    /// </summary>
    public static class FilePathProvider
    {
        private static readonly Dictionary<EntityType, string> _entitytype = new Dictionary<EntityType, string>();

        /// <summary>
        /// 生成唯一文件名（GUID + 原扩展名）
        /// </summary>
        /// <param name="originalFileName">原始文件名</param>
        /// <returns>文件名</returns>
        private static string GenerateUniqueFileName(string originalFileName)
        {
            if (string.IsNullOrWhiteSpace(originalFileName))
            {
                throw new ArgumentNullException(nameof(originalFileName), "原始文件名不能为空");
            }
            var extension = Path.GetExtension(originalFileName);
            var uniqueName = $"{Guid.NewGuid()}{extension}";

            return uniqueName;
        }

        /// <summary>
        /// 确保目录文件夹存在，不存在则创建
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        private static void EnsureDirectoryExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        /// <summary>
        /// 获取文件类型（测试待完善）
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        private static string GetFileType(string fileType)
        {
            return "test";
        }

        /// <summary>
        /// 生成完整的文件存储路径（核心方法）
        /// </summary>
        /// <param name="originalFileName"></param>
        /// <param name="fileType"></param>
        /// <returns>文件相对路径</returns>
        public static string GenerateFullFilePath(string originalFileName, string fileType)
        {
            var uniqueFileName = GenerateUniqueFileName(originalFileName);
            var directoryPath = Path.Combine("_rootPath", GetFileType(fileType));

            EnsureDirectoryExists(directoryPath);
            var fullFilePath = Path.Combine(directoryPath, uniqueFileName);

            return fullFilePath;
        }

        public static string GenerateFullFilePath(IFormFile file, string fileType)
        {
            var originalFileName = file.FileName;
            return GenerateFullFilePath(originalFileName, fileType);
        }
    }
}
