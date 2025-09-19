using CloudApp.Core.Entities;
using CloudApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudApp.Service.Services
{
    public class TrackRepository : BaseRepository<Track>
    {
        private readonly MyDBContext _dbContext;
        public TrackRepository(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override IEnumerable<Track> GetAllEntities()
        {
            return _dbContext.Tracks.Include(t => t.Album).ToList();
        }

    }
}
