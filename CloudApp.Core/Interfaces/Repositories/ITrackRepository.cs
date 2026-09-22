using CloudApp.Core.Entities;

namespace CloudApp.Core.Interfaces.Repositories
{
    public interface ITrackRepository : IRepository<Track>
    {
        Track? FindByTitle(string trackTitle);

        bool TrackExists(string trackTitle, int? albumId = null);

        /// <summary>
        /// 根据标题查询曲目
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        IEnumerable<Track> GetTracksByTitle(string title);

        /// <summary>
        /// 根据专辑ID查询曲目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<Track> GetTracksByAlbumId(int id);

        #region 异步方法

        /// <summary>
        /// 根据标题查询曲目（异步，模糊匹配）
        /// </summary>
        Task<IEnumerable<Track>> GetTracksByTitleAsync(string title, CancellationToken ct = default);

        /// <summary>
        /// 根据专辑ID查询曲目（异步）
        /// </summary>
        Task<IEnumerable<Track>> GetTracksByAlbumIdAsync(int albumId, CancellationToken ct = default);

        /// <summary>
        /// 根据标题精确查询曲目（异步）
        /// </summary>
        Task<Track?> FindByTitleAsync(string trackTitle, CancellationToken ct = default);

        /// <summary>
        /// 判断同名单曲是否存在（异步）。
        /// </summary>
        /// <param name="trackTitle">单曲标题</param>
        /// <param name="albumId">专辑ID，传入则限定在该专辑内判断同重</param>
        /// <param name="excludeId">需要排除的单曲ID（更新场景下排除自身）</param>
        /// <param name="ct"></param>
        Task<bool> TrackExistsAsync(string trackTitle, int? albumId = null, int? excludeId = null, CancellationToken ct = default);

        #endregion
    }
}
