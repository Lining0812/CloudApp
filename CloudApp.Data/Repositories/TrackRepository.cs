using CloudApp.Core.Entities;
using CloudApp.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CloudApp.Data.Repositories
{
    public class TrackRepository : BaseRepository<Track>, ITrackRepository
    {
        public TrackRepository(MyDBContext dbContext) : base(dbContext)
        {
        }

        #region 同步方法
        public override IEnumerable<Track> GetAll()
        {
            return _dbSet.Include(t => t.Album).ToList();
        }

        public override Track GetById(int id)
        {
            return _dbSet.Include(t => t.Album).FirstOrDefault(t => t.Id == id);
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

        public IEnumerable<Track> GetTracksByAlbumId(int albumId)
        {
            return _dbSet
                .Include(t => t.Album)
                .Where(t => t.AlbumId == albumId)
                .ToList();
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
