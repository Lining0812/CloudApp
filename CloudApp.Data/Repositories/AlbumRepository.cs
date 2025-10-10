using CloudApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudApp.Data.Repositories
{
    public class AlbumRepository : BaseRepository<Album>
    {
        public AlbumRepository(MyDBContext dbContext) : base(dbContext)
        {
        }

        #region 重写基础仓储实现
        public override IEnumerable<Album> GetAll()
        {
            return _dbSet.Include(a => a.Tracks).ToList();
        }

        public override Album GetById(int id)
        {
            return _dbSet.Include(a => a.Tracks).FirstOrDefault(a => a.Id == id);
        }

        //public override async Task<IEnumerable<Album>> GetAllAsync()
        //{
        //    return await _dbSet.Include(a => a.Tracks).ToListAsync();
        //}

        //public override async Task<Album> GetByIdAsync(int id)
        //{
        //    return await _dbSet.Include(a => a.Tracks).FirstOrDefaultAsync(a => a.Id == id);
        //}
        #endregion

        #region Album特有查询方法
        // 根据标题查询专辑
        public IEnumerable<Album> GetAlbumsByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                return Enumerable.Empty<Album>();
            
            return _dbSet
                .Include(a => a.Tracks)
                .Where(a => a.Title.Contains(title))
                .ToList();
        }

        // 异步根据标题查询专辑
        public async Task<IEnumerable<Album>> GetAlbumsByTitleAsync(string title)
        {
            if (string.IsNullOrEmpty(title))
                return Enumerable.Empty<Album>();
            
            return await _dbSet
                .Include(a => a.Tracks)
                .Where(a => a.Title.Contains(title))
                .ToListAsync();
        }

        // 根据艺术家查询专辑
        public IEnumerable<Album> GetAlbumsByArtist(string artist)
        {
            if (string.IsNullOrEmpty(artist))
                return Enumerable.Empty<Album>();
            
            return _dbSet
                .Include(a => a.Tracks)
                .Where(a => a.Artist.Contains(artist))
                .ToList();
        }

        // 异步根据艺术家查询专辑
        public async Task<IEnumerable<Album>> GetAlbumsByArtistAsync(string artist)
        {
            if (string.IsNullOrEmpty(artist))
                return Enumerable.Empty<Album>();
            
            return await _dbSet
                .Include(a => a.Tracks)
                .Where(a => a.Artist.Contains(artist))
                .ToListAsync();
        }

        // 获取专辑及其曲目数量
        public Dictionary<Album, int> GetAlbumsWithTrackCount()
        {
            return _dbSet
                .Include(a => a.Tracks)
                .ToDictionary(a => a, a => a.Tracks.Count);
        }

        //// 分页获取专辑，包含曲目
        //public override IEnumerable<Album> GetPaged(int pageNumber, int pageSize)
        //{
        //    return _dbSet
        //        .Include(a => a.Tracks)
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToList();
        //}

        //// 异步分页获取专辑，包含曲目
        //public override async Task<IEnumerable<Album>> GetPagedAsync(int pageNumber, int pageSize)
        //{
        //    return await _dbSet
        //        .Include(a => a.Tracks)
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToListAsync();
        //}
        #endregion
    }
}
