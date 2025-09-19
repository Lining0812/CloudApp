using CloudApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CloudApp.Service.Services
{
    public class AlbumSevice : BaseService<Album>
    {
        // 使用DTO类型写入
        public void AddAlbum(Album album)
        {
            this.Add(album);
        }
    }
}
