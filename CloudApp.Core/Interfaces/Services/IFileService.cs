using CloudApp.Core.Entities;

namespace CloudApp.Core.Interfaces.Services
{
    /// <summary>
    /// 文件服务接口
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="fileName">文件名</param>
        /// <returns>上传后的文件记录</returns>
        Task<UploadedFile> UploadAsync(Stream stream, string fileName, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据ID下载文件
        /// </summary>
        /// <param name="id">文件ID</param>
        /// <returns>文件流和文件名</returns>
        Task<(Stream Stream, string FileName)> DownloadAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据ID获取文件信息
        /// </summary>
        /// <param name="id">文件ID</param>
        /// <returns>文件记录</returns>
        Task<UploadedFile?> GetFileInfoAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除文件（从存储和数据库）
        /// </summary>
        /// <param name="id">文件ID</param>
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// 搜索文件
        /// </summary>
        /// <param name="fileName">文件名关键词</param>
        /// <param name="skip">跳过数量</param>
        /// <param name="take">获取数量</param>
        /// <returns>文件列表</returns>
        Task<IEnumerable<UploadedFile>> SearchAsync(string fileName, int skip = 0, int take = 20, CancellationToken cancellationToken = default);

        /// <summary>
        /// 分页获取文件列表
        /// </summary>
        /// <param name="skip">跳过数量</param>
        /// <param name="take">获取数量</param>
        /// <returns>文件列表</returns>
        Task<IEnumerable<UploadedFile>> GetPagedAsync(int skip = 0, int take = 20, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取文件总数
        /// </summary>
        Task<int> CountAsync(CancellationToken cancellationToken = default);
    }
}
