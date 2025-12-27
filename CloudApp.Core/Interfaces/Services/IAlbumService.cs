using CloudApp.Core.Dtos;

namespace CloudApp.Core.Interfaces
{
    public interface IAlbumService
    {
        /// <summary>
        /// 创建专辑
        /// </summary>
        /// <param name="album"></param>
        /// <returns></returns>
        void AddAlbum(CreateAlbumDto album);

        /// <summary>
        /// 获取所有专辑信息
        /// </summary>
        /// <returns></returns>
        ICollection<AlbumInfoDto> GetAllAlbums();

        /// <summary>
        /// 根据Id获取专辑信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AlbumInfoDto GetAlbumById(int id);

        /// <summary>
        /// 根据Id更新专辑信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="album"></param>
        void UpdateAlbum(int id, CreateAlbumDto album);

        /// <summary>
        /// 根据Id删除专辑
        /// </summary>
        /// <param name="id"></param>
        void DeleteAlbum(int id);
    }
}
