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
    public class AlbumService : IAlbumService
    {
        private readonly IRepository<Album> _albumRepository;

        public AlbumService(IRepository<Album> albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public void AddAlbum(CreateAlbumDto model)
        {
            Album album = new Album
            {
                Title = model.Title,
                Description = model.Description,
                Artist = model.Artist,
                ReleaseDate = DateTime.UtcNow,
                CoverImageUrl = model.CoverImageUrl
            };

            this._albumRepository.AddEntity(album);
        }

        public void DeleteAlbum(int id)
        {
            this._albumRepository.DeleteEntity(id);
        }

        public AlbumInfoDto GetAlbumById(int id)
        {
            var album = this._albumRepository.GetEntityById(id);

            return new AlbumInfoDto(album);
        }

        public ICollection<AlbumInfoDto> GetAllAlbums()
        {
            var albums = this._albumRepository.GetAllEntities();

            return albums.Select(a => new AlbumInfoDto(a)).ToList();
        }

        public void UpdateAlbum(int id, CreateAlbumDto model)
        {
             Album album = _albumRepository.GetEntityById(id);
            if (album != null)
            {
                album.Title = model.Title;
                album.Description = model.Description;
                album.Artist = model.Artist;
                album.CoverImageUrl = model.CoverImageUrl;
                _albumRepository.UpdateEntity(album);
            }
        }
    }
}
