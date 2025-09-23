using CloudApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudApp.Data.Repository
{
    public class AlbumRepository : BaseRepository<Album>
    {
        private readonly MyDBContext _dbContext;
        public AlbumRepository(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        private void InitializeSampleData()
        {
            if (GetAllEntities().Any())
            {
                var album = new Album { Title = "初始化专辑" };
                AddEntity(album);
            }
        }

        public override IEnumerable<Album> GetAllEntities()
        {
            return _dbContext.Albums.Include(a => a.Tracks).ToList();
        }

        public override Album GetEntityById(int id)
        {
            var album = _dbContext.Albums.Include(a => a.Tracks).FirstOrDefault(a => a.Id == id);

            return album;
        }
    }
}
