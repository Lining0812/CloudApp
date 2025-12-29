using CloudApp.Core.Entities;
using CloudApp.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CloudApp.Data.Repositories
{
    public class ConcertRepository : BaseRepository<Concert>, IConcertRepository
    {
        public ConcertRepository(MyDBContext dbContext)
            :base(dbContext)
        {
        }

        #region 同步方法
        public override IEnumerable<Concert> GetAll()
        {
            return _dbSet.Include(a => a.Tracks).Where(a => !a.IsDeleted).ToList();
        }

        public override Concert GetById(int id)
        {
            return _dbSet.Include(c => c.Tracks)
                         .FirstOrDefault(c => c.Id == id && !c.IsDeleted);
        }
        #endregion
    }
}
