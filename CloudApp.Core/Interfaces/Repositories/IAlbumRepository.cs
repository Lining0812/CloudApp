using CloudApp.Core.Entities;

namespace CloudApp.Core.Interfaces.Repositories
{
    public interface IAlbumRepository : IRepository<Album>
    {
        /// <summary>
        /// 根据专辑标题查找专辑
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        Album? FindByTitle(string title);

        /// <summary>
        /// 根据专辑标题判断是否存在
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        bool AlbumExists(string title);
    }
}
