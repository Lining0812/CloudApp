using CloudApp.Core.Dtos.Album;

namespace CloudApp.Core.Interfaces.Services
{
    public interface IAlbumService
    {
        /// <summary>
        /// 创建专辑
        /// </summary>
        /// <param name="album"></param>
        /// <returns></returns>
        void CreateAlbum(CreateAlbumRequest album);

        /// <summary>
        /// 获取所有专辑信息
        /// </summary>
        /// <returns></returns>
        ICollection<AlbumInfoDto> GetAllAlbums();

        /// <summary>
        /// 根据Id更新专辑信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="album"></param>
        void UpdateAlbum(int id, CreateAlbumRequest album);

        /// <summary>
        /// 根据Id删除专辑
        /// </summary>
        /// <param name="id"></param>
        void DeleteAlbum(int id);
    }
}
