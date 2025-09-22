using CloudApp.Core.Dtos;

namespace CloudApp.Core.Interface
{
    public interface IAlbumService
    {
        /// <summary>
        /// 添加专辑
        /// </summary>
        /// <param name="album"></param>
        void AddAlbum(CreateAlbumDto album);
        /// <summary>
        /// 获取所有专辑信息
        /// </summary>
        /// <returns></returns>
        ICollection<AlbumInfoDto> GetAllAlbums();
        /// <summary>
        /// 根据ID获取专辑信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AlbumInfoDto GetAlbumById(int id);
        /// <summary>
        /// 根据ID查找并更新专辑信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="album"></param>
        void UpdateAlbum(int id, CreateAlbumDto album);
        /// <summary>
        /// 根据ID删除专辑
        /// </summary>
        /// <param name="id"></param>
        void DeleteAlbum(int id);
    }
}
