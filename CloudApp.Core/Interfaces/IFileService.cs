using Microsoft.AspNetCore.Http;

namespace CloudApp.Core.Interfaces
{
    public interface IFileService
    {
        /// <summary>
        /// 上传文件方法
        /// </summary>
        /// <param name="file"></param>
        /// <param name="UploadDirectory"></param>
        /// <returns></returns>
        string UploadFile(IFormFile file, string UploadDirectory);
        /// <summary>
        /// 验证文件的扩展名和大小
        /// </summary>
        /// <param name="file"></param>
        /// <param name="allowedExtensions"></param>
        /// <param name="maxFileSizeBytes"></param>
        /// <returns></returns>
        bool ValidateFile(IFormFile file, string[] allowedExtensions, long maxFileSizeBytes);
        /// <summary>
        /// 确保上传路径存在，如果不存在则创建
        /// </summary>
        /// <param name="UploadDirectory"></param>
        /// <returns></returns>
        string EnsureUploadPath(string UploadDirectory);
        /// <summary>
        /// 生成唯一的文件名
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        string GenerateFileName(IFormFile file);
        /// <summary>
        /// 保存文件到指定路径
        /// </summary>
        /// <param name="file"></param>
        /// <param name="filePath"></param>
        void SaveFile(IFormFile file, string filePath);
    }
}
