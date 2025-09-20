using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using CloudApp.Data.Repository;
using CloudApp.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CloudApp.Service.Services
{
    public class AlbumSevice : IAlbumService
    {
        private readonly IRepository<Album> _albumRepository;

        public AlbumSevice(IRepository<Album> albumRepository)
        {
            _albumRepository = albumRepository;
        }

        // 使用DTO类型写入
        public void AddAlbum(Album album)
        {
            this._albumRepository.AddEntity(album);
        }
        public IEnumerable<AlbumInfoDto> GetAllAlbums()
        {
            var albums = this._albumRepository.GetAllEntities();

            return albums.Select(a => new AlbumInfoDto
            {
                Id = a.Id,
                Title = a.Title,
                Tracks = a.Tracks.Select(t => t.Title).ToList()
            }).ToList();
        }
    }
}
