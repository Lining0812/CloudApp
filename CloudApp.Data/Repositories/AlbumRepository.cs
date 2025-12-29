using CloudApp.Core.Entities;
using CloudApp.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CloudApp.Data.Repositories
{
    public class AlbumRepository : BaseRepository<Album>, IAlbumRepository
    {
        public AlbumRepository(MyDBContext dbContext) 
            :base(dbContext)
        {
        }

        #region 同步方法
        public override IEnumerable<Album> GetAll()
        {
            return _dbSet.Include(a => a.Tracks).Where(a => !a.IsDeleted).ToList();
        }

        public override Album GetById(int id)
        {
            return _dbSet.Include(a => a.Tracks)
                         .FirstOrDefault(a => a.Id == id && !a.IsDeleted);
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
