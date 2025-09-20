using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudApp.Service.Interfaces
{
    public interface IAlbumService
    {
        void AddAlbum(Album album);
        IEnumerable<AlbumInfoDto> GetAllAlbums();
    }
}
