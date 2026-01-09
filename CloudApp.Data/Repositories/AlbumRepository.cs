using CloudApp.Core.Entities;
using CloudApp.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CloudApp.Data.Repositories
{
    public class AlbumRepository : BaseRepository<Album>, IAlbumRepository
    {
        public AlbumRepository(MyDBContext dbContext, ILogger<AlbumRepository> logger) 
            :base(dbContext, logger)
        {
        }

        #region 同步方法
        public override IEnumerable<Album> GetAll()
        {
            // BaseRepository已经通过全局查询过滤器过滤了IsDeleted，这里只需要Include导航属性
            return _dbSet.Include(a => a.Tracks).ToList();
        }

        public override Album? GetById(int id)
        {
            // BaseRepository已经通过全局查询过滤器过滤了IsDeleted，这里只需要Include导航属性
            return _dbSet.Include(a => a.Tracks)
                         .FirstOrDefault(a => a.Id == id);
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
