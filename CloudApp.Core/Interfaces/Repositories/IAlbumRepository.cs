using CloudApp.Core.Dtos.Album;
using CloudApp.Core.Entities;

namespace CloudApp.Core.Interfaces.Repositories
{
    public interface IAlbumRepository : IRepository<Album>
    {
        /// <summary>
        /// 根据专辑标题查找专辑
        /// </summary>
        Album? FindAlbumByTitle(string title);

        /// <summary>
        /// 根据专辑标题判断是否存在
        /// </summary>
        bool AlbumExists(string title);

        /// <summary>
        /// 获取所有专辑的投影（仅查询需要的字段）
        /// </summary>
        IEnumerable<AlbumInfoDto> GetAllAsDto();
    }
}
