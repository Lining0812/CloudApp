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
            return _dbSet.Include(a => a.Tracks).FirstOrDefault(a => a.Id == id && !a.IsDeleted);
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
    }
}
