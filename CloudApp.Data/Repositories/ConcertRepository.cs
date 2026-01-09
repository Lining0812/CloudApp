using CloudApp.Core.Entities;
using CloudApp.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CloudApp.Data.Repositories
{
    public class ConcertRepository : BaseRepository<Concert>, IConcertRepository
    {
        public ConcertRepository(MyDBContext dbContext, ILogger<ConcertRepository> logger)
            :base(dbContext, logger)
        {
        }

        #region 同步方法
        public override IEnumerable<Concert> GetAll()
        {
            // 全局查询过滤器已经自动过滤IsDeleted
            return _dbSet.Include(a => a.Tracks).ToList();
        }

        public override Concert? GetById(int id)
        {
            // 全局查询过滤器已经自动过滤IsDeleted
            return _dbSet.Include(c => c.Tracks)
                         .FirstOrDefault(c => c.Id == id);
        }
        #endregion
    }
}
