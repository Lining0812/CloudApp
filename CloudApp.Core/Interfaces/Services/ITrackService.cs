using CloudApp.Core.Dtos.Track;
using CloudApp.Core.Entities;

namespace CloudApp.Core.Interfaces.Services
{
    public interface ITrackService
    {
        #region ͬ同步方法
        /// <summary>
        /// 添加单曲
        /// </summary>
        /// <param name="model"></param>
        void AddTrack(CreateTrackDto model);

        /// <summary>
        /// 根据Id更新专辑信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        void UpdateTrack(int id, CreateTrackDto model);

        /// <summary>
        /// 根据Id删除单曲
        /// </summary>
        /// <param name="id"></param>
        void DeleteTrack(int id);

        /// <summary>
        /// 获取所有单曲
        /// </summary>
        /// <returns></returns>
        ICollection<TrackInfoDto> GetAllTracks();

        /// <summary>
        /// 根据Id获取专辑信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TrackInfoDto GetById(int id);

        /// <summary>
        /// 根据AlbumId获取所属专辑
        /// </summary>
        /// <returns></returns>
        ICollection<Track> GetByAlbumdID();
        #endregion
    }
}
