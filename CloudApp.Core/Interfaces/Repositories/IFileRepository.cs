using CloudApp.Core.Entities;

namespace CloudApp.Core.Interfaces.Repositories
{
    public interface IFileRepository : IRepository<UploadedFile>
    {
        /// <summary>
        /// 根据文件大小和哈希查找文件
        /// </summary>
        Task<UploadedFile?> FindFileAsync(long size, string hash);

        /// <summary>
        /// 根据文件名搜索文件（模糊匹配）
        /// </summary>
        Task<IEnumerable<UploadedFile>> SearchByFileNameAsync(string fileName, int skip = 0, int take = 20);

        /// <summary>
        /// 获取所有文件（分页）
        /// </summary>
        Task<IEnumerable<UploadedFile>> GetPagedAsync(int skip = 0, int take = 20);
    }
}
