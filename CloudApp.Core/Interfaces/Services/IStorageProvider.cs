using Microsoft.AspNetCore.Http;

namespace CloudApp.Core.Interfaces.Services
{
    /// <summary>
    /// 存储提供者接口
    /// </summary>
    public interface IStorageProvider
    {
        /// <summary>
        /// 存储文件至目录
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="type">实体类型</param>
        /// <returns>文件的相对路径</returns>
        string SaveFile(IFormFile file, string type);

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <returns></returns>
        Stream ReadFile();

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filepath"></param>
        void DeleteFile(string filepath);

        /// <summary>
        /// 更新文件
        /// </summary>
        /// <param name="filepath"></param>
        void UpdateFile(string filepath);
    }
}
