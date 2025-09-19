using CloudApp.Core.Entities;
using CloudApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudApp.Service.Services
{
    public class AlbumRepository : BaseRepository<Album>
    {
        public AlbumRepository(MyDBContext dbContext) : base(dbContext)
        {
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            if (GetAllEntities().Any())
            {
                var album = new Album { Title = "初始化专辑" };
                AddEntity(album);
            }
        }
    }
}
