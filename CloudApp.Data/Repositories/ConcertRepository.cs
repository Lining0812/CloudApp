using CloudApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudApp.Data.Repositories
{
    public class ConcertRepository : BaseRepository<Concert>
    {
        public ConcertRepository(MyDBContext dbContext)
            :base(dbContext)
        {
        }

        #region 同步方法

        #endregion
    }
}
