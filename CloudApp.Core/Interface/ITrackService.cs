using CloudApp.Core.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudApp.Core.Interface
{
    public interface ITrackService
    {
        #region 同步方法
        void AddTrack(CreateTrackDto model);
        ICollection<TrackInfoDto> GetAllTracks();
        
        /// <summary>
        /// 根据ID获取轨道信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TrackInfoDto GetTrackById(int id);
        
        /// <summary>
        /// 更新轨道信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        void UpdateTrack(int id, CreateTrackDto model);
        
        /// <summary>
        /// 删除轨道
        /// </summary>
        /// <param name="id"></param>
        void DeleteTrack(int id);
        #endregion

        #region 异步方法（新增）
        /// <summary>
        /// 异步添加轨道
        /// </summary>
        /// <param name="model"></param>
        //Task AddTrackAsync(CreateTrackDto model);
        
        /// <summary>
        /// 异步获取所有轨道
        /// </summary>
        /// <returns></returns>
        //Task<ICollection<TrackInfoDto>> GetAllTracksAsync();
        
        /// <summary>
        /// 异步根据ID获取轨道信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //Task<TrackInfoDto> GetTrackByIdAsync(int id);
        
        /// <summary>
        /// 异步更新轨道信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        //Task UpdateTrackAsync(int id, CreateTrackDto model);
        
        /// <summary>
        /// 异步删除轨道
        /// </summary>
        /// <param name="id"></param>
        //Task DeleteTrackAsync(int id);
        #endregion

        #region 扩展功能（新增）
        /// <summary>
        /// 根据专辑ID获取单曲
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        ICollection<TrackInfoDto> GetTracksByAlbumId(int albumId);
        
        /// <summary>
        /// 异步根据专辑ID获取轨道
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        //Task<ICollection<TrackInfoDto>> GetTracksByAlbumIdAsync(int albumId);
        
        /// <summary>
        /// 分页获取轨道
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        //ICollection<TrackInfoDto> GetTracksWithPagination(int pageNumber, int pageSize);
        
        /// <summary>
        /// 异步分页获取轨道
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        //Task<ICollection<TrackInfoDto>> GetTracksWithPaginationAsync(int pageNumber, int pageSize);
        #endregion
    }
}
