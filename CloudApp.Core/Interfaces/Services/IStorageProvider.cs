using CloudApp.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace CloudApp.Core.Interfaces.Services
{
    /// <summary>
    /// 存储提供者接口
    /// </summary>
    public interface IStorageProvider
    {
        /// <summary>
        /// 存储文件
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="type">实体类型</param>
        /// <returns>文件的相对路径</returns>
        string SaveFile(IFormFile file, Entype type);

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="filepath">文件路径（相对路径或绝对路径）</param>
        /// <returns>文件流</returns>
        Stream ReadFile(string filepath);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filepath">文件路径（相对路径或绝对路径）</param>
        void DeleteFile(string filepath);

        /// <summary>
        /// 更新文件
        /// </summary>
        /// <param name="filepath">要替换的文件路径</param>
        /// <param name="newFile">新文件</param>
        /// <returns>更新后的文件路径</returns>
        string UpdateFile(string filepath, IFormFile newFile);
    }
}
