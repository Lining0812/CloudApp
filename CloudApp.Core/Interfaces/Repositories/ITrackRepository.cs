using CloudApp.Core.Entities;

namespace CloudApp.Core.Interfaces.Repositories
{
    public interface ITrackRepository : IRepository<Track>
    {
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

    }
}
