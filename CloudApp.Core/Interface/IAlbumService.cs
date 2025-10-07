using CloudApp.Core.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudApp.Core.Interface
{
    public interface IAlbumService
    {
        #region 同步方法
        /// <summary>
        /// 添加专辑
        /// </summary>
        /// <param name="album"></param>
        int AddAlbum(CreateAlbumDto album);
        
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
        /// 根据ID更新已有专辑信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="album"></param>
        void UpdateAlbum(int id, CreateAlbumDto album);
        
        /// <summary>
        /// 根据ID删除专辑
        /// </summary>
        /// <param name="id"></param>
        void DeleteAlbum(int id);
        #endregion

        #region 异步方法（新增）
        /// <summary>
        /// 异步添加专辑
        /// </summary>
        /// <param name="album"></param>
        //Task AddAlbumAsync(CreateAlbumDto album);
        
        /// <summary>
        /// 异步获取所有专辑信息
        /// </summary>
        /// <returns></returns>
        //Task<ICollection<AlbumInfoDto>> GetAllAlbumsAsync();
        
        /// <summary>
        /// 异步根据ID获取专辑信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //Task<AlbumInfoDto> GetAlbumByIdAsync(int id);
        
        /// <summary>
        /// 异步根据ID更新已有专辑信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="album"></param>
        //Task UpdateAlbumAsync(int id, CreateAlbumDto album);
        
        /// <summary>
        /// 异步根据ID删除专辑
        /// </summary>
        /// <param name="id"></param>
        //Task DeleteAlbumAsync(int id);
        #endregion

        #region 扩展功能（新增）
        /// <summary>
        /// 根据标题查询专辑
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        //ICollection<AlbumInfoDto> GetAlbumsByTitle(string title);
        
        /// <summary>
        /// 异步根据标题查询专辑
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        //Task<ICollection<AlbumInfoDto>> GetAlbumsByTitleAsync(string title);
        
        /// <summary>
        /// 分页获取专辑
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        //ICollection<AlbumInfoDto> GetAlbumsWithPagination(int pageNumber, int pageSize);
        
        /// <summary>
        /// 异步分页获取专辑
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        //Task<ICollection<AlbumInfoDto>> GetAlbumsWithPaginationAsync(int pageNumber, int pageSize);
        #endregion
    }
}
