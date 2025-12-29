using CloudApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudApp.Data.Repositories
{
    public class TrackRepository : BaseRepository<Track>
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

        //public override async Task<IEnumerable<Track>> GetAllAsync()
        //{
        //    return await _dbSet.Include(t => t.Album).ToListAsync();
        //}

        //public override async Task<Track> GetByIdAsync(int id)
        //{
        //    return await _dbSet.Include(t => t.Album).FirstOrDefaultAsync(t => t.Id == id);
        //}
        #endregion
        // 根据标题查询曲目
        public IEnumerable<Track> GetTracksByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                return Enumerable.Empty<Track>();
            
            return _dbSet
                .Include(t => t.Album)
                .Where(t => t.Title.Contains(title))
                .ToList();
        }

        // 根据专辑ID查询曲目
        public ICollection<Track> GetTracksByAlbumId(int albumId)
        {
            return _dbSet
                .Include(t => t.Album)
                .Where(t => t.AlbumId == albumId)
                .ToList();
        }
    }
}
