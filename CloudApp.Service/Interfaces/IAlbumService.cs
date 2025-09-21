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
        void AddAlbum(CreateAlbumDto album);

        /// <summary>
        /// 获取所有专辑信息
        /// </summary>
        /// <returns></returns>
        ICollection<AlbumInfoDto> GetAllAlbums();
        AlbumInfoDto GetAlbumById(int id);
        void UpdateAlbum(int id, CreateAlbumDto album);
        void DeleteAlbum(int id);
    }
}
