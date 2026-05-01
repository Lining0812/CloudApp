using CloudApp.Core.Dtos.Album;
using CloudApp.Core.Entities;
using CloudApp.Core.Extensions;
using CloudApp.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CloudApp.Infrastructure.Repositories
{
    public class AlbumRepository : BaseRepository<Album>, IAlbumRepository
    {
        public AlbumRepository(MyDBContext dbContext, ILogger<AlbumRepository> logger)
            : base(dbContext, logger)
        {
        }

        #region 同步方法
        public override IEnumerable<Album> GetAll()
        {
            return _dbSet.Include(a => a.Tracks).ToList();
        }

        public IEnumerable<AlbumInfoDto> GetAllAsDto()
        {
            // 查询没能获取关联的单曲
            //return _dbSet.Select(a => a.ToInfoDto()).ToList();

            return _dbSet.Select(a => new AlbumInfoDto
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                Artist = a.Artist,
                ReleaseDate = a.ReleaseDate,
                Tracks = a.Tracks.Select(t => t.Title).ToList(),
            }).ToList();
        }

        public override Album? GetById(int id)
        {
            return _dbSet.Include(a => a.Tracks)
                         .FirstOrDefault(a => a.Id == id);
        }

        public Album? FindAlbumByTitle(string title)
        {
            if (string.IsNullOrEmpty(title)) return null;

            return _dbSet.Include(a => a.Tracks)
                         .FirstOrDefault(a => a.Title.ToLower() == title.ToLower());
        }

        public bool AlbumExists(string title)
        {
            if (string.IsNullOrEmpty(title)) return false;
            return _dbSet.Any(a => a.Title == title);
        }

        #endregion

        #region 异步方法
        //public override async Task<IEnumerable<Album>> GetAllAsync()
        //{
        //    return await _dbSet.Include(a => a.Tracks).ToListAsync();
        //}

        //public override async Task<Album> GetByIdAsync(int id)
        //{
        //    return await _dbSet.Include(a => a.Tracks).FirstOrDefaultAsync(a => a.Id == id);
        //}
        #endregion
    }
}
