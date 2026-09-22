using CloudApp.Core.Dtos.Track;

namespace CloudApp.Core.Interfaces.Services
{
    /// <summary>
    /// 单曲服务
    /// </summary>
    public interface ITrackService
    {
        #region 写入

        /// <summary>
        /// 添加单曲，返回创建后的完整信息（含自增 Id）
        /// </summary>
        Task<TrackInfoDto> CreateTrackAsync(TrackCreateDto model, CancellationToken ct = default);

        /// <summary>
        /// 根据Id更新单曲（局部更新：DTO 中为 null 的字段不修改）
        /// </summary>
        Task<TrackInfoDto> UpdateTrackAsync(int id, TrackUpdateDto model, CancellationToken ct = default);

        /// <summary>
        /// 根据Id软删除单曲
        /// </summary>
        Task DeleteTrackAsync(int id, CancellationToken ct = default);

        #endregion

        #region 查询

        /// <summary>
        /// 获取所有单曲
        /// </summary>
        Task<ICollection<TrackInfoDto>> GetAllTracksAsync(CancellationToken ct = default);

        /// <summary>
        /// 根据Id获取单曲详情，不存在时抛 EntityNotFoundException
        /// </summary>
        Task<TrackInfoDto> GetByIdAsync(int id, CancellationToken ct = default);

        /// <summary>
        /// 根据专辑Id获取曲目，专辑不存在时抛 EntityNotFoundException
        /// </summary>
        Task<ICollection<TrackInfoDto>> GetTracksByAlbumIdAsync(int albumId, CancellationToken ct = default);

        /// <summary>
        /// 按标题模糊搜索单曲
        /// </summary>
        Task<ICollection<TrackInfoDto>> SearchTracksAsync(string keyword, CancellationToken ct = default);

        #endregion
    }
}
