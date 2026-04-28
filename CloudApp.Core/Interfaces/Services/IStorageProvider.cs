using CloudApp.Core.Entities;
using CloudApp.Core.Enums;

namespace CloudApp.Core.Interfaces.Services
{
    /// <summary>
    /// 存储提供者接口
    /// </summary>
    public interface IStorageProvider
    {
        public StorageType StorageType { get; }

        /// <summary>
        /// 存储文件（通过 key 和 stream）
        /// </summary>
        /// <param name="key">文件键（相对路径）</param>
        /// <param name="stream">文件流</param>
        /// <returns>文件访问 URI</returns>
        Uri SaveFile(string key, Stream stream);

        /// <summary>
        /// 异步存储文件（通过 key 和 stream）
        /// </summary>
        /// <param name="key">文件键（相对路径）</param>
        /// <param name="stream">文件流</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>文件访问 URI</returns>
        Task<Uri> SaveFileAsync(string key, Stream stream, CancellationToken cancellationToken = default);

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="filepath">文件路径（相对路径或绝对路径）</param>
        /// <returns>文件流</returns>
        Stream ReadFile(string filepath);

        /// <summary>
        /// 异步读取文件
        /// </summary>
        /// <param name="filepath">文件路径（相对路径或绝对路径）</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>文件流</returns>
        Task<Stream> ReadFileAsync(string filepath, CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filepath">文件路径（相对路径或绝对路径）</param>
        void DeleteFile(string filepath);

        /// <summary>
        /// 异步删除文件
        /// </summary>
        /// <param name="filepath">文件路径（相对路径或绝对路径）</param>
        /// <param name="cancellationToken">取消令牌</param>
        Task DeleteFileAsync(string filepath, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新文件
        /// </summary>
        /// <param name="filepath">要替换的文件路径</param>
        /// <param name="newFile">新文件</param>
        /// <returns>更新后的文件路径</returns>
        string UpdateFile(string filepath, UploadedFile newFile);

        /// <summary>
        /// 异步更新文件
        /// </summary>
        /// <param name="filepath">要替换的文件路径</param>
        /// <param name="newFile">新文件</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>更新后的文件路径</returns>
        Task<string> UpdateFileAsync(string filepath, UploadedFile newFile, CancellationToken cancellationToken = default);

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <returns>是否存在</returns>
        bool FileExists(string filepath);

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <returns>文件信息，若不存在则返回 null</returns>
        FileInfo? GetFileInfo(string filepath);
    }
}
