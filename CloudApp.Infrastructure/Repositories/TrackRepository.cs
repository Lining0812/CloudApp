using CloudApp.Core.Entities;
using CloudApp.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CloudApp.Infrastructure.Repositories
{
    public class TrackRepository : BaseRepository<Track>, ITrackRepository
    {
        public TrackRepository(MyDBContext dbContext, ILogger<TrackRepository> logger) : base(dbContext, logger)
        {
        }

        #region 同步方法
        public override IEnumerable<Track> GetAll()
        {
            return _dbSet.Include(t => t.Album).ToList();
        }

        public override Track? GetById(int id)
        {
            // 全局查询过滤器已经自动过滤IsDeleted
            return _dbSet.Include(t => t.Album).FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Track> GetTracksByAlbumId(int albumId)
        {
            return _dbSet.Include(t => t.Album).Where(t => t.AlbumId == albumId).ToList();
        }

        public IEnumerable<Track> GetTracksByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                return Enumerable.Empty<Track>();

            return _dbSet
                .Include(t => t.Album)
                .Where(t => t.Title.Contains(title))
                .ToList();
        }

        public Track? FindByTitle(string trackTitle)
        {
            if (string.IsNullOrEmpty(trackTitle)) return null;
            return _dbSet.Include(t => t.Album).FirstOrDefault(a => a.Title == trackTitle);
        }

        public bool TrackExists(string trackTitle, int? albumId = null)
        {
            if (string.IsNullOrEmpty(trackTitle)) return false;

            if (albumId.HasValue)
            {
                return _dbSet.Any(t => t.Title == trackTitle && t.AlbumId == albumId.Value);
            }

            return _dbSet.Any(t => t.Title == trackTitle);
        }
        #endregion

        #region 异步方法
        //public override async Task<IEnumerable<Track>> GetAllAsync()
        //{
        //    return await _dbSet.Include(t => t.Album).ToListAsync();
        //}

        //public override async Task<Track> GetByIdAsync(int id)
        //{
        //    return await _dbSet.Include(t => t.Album).FirstOrDefaultAsync(t => t.Id == id);
        //}
        #endregion
    }
}
