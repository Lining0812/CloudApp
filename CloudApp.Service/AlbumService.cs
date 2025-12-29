using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using CloudApp.Core.Extensions;
using CloudApp.Core.Interfaces.Repositories;
using CloudApp.Core.Interfaces.Services;

namespace CloudApp.Service
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository repository)
        {
            _albumRepository = repository;
        }

        #region 同步方法
        public void AddAlbum(CreateAlbumDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            Album album = dto.ToEntity();
            _albumRepository.Add(album);
            _albumRepository.SaveChange();
        }

        public void DeleteAlbum(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("专辑ID无效", nameof(id));
            }
            bool albumExists = _albumRepository.Exists(id);
            if (!albumExists)
            {
                throw new ArgumentException($"专辑不存在 (ID: {id})");
            }
            _albumRepository.Delete(id);
            _albumRepository.SaveChange();
        }

        public void UpdateAlbum(int id, CreateAlbumDto model)
        {
            Album album = _albumRepository.GetById(id);
            if (album == null)
            {
               throw new ArgumentException($"专辑不存在 (ID: {id})");
            }
            else
            {
                album.Title = model.Title;
                album.Description = model.Description;
                album.Artist = model.Artist;
                album.ReleaseDate = model.ReleaseDate;
                album.UpdatedAt = DateTime.UtcNow;

                _albumRepository.Update(album);
                _albumRepository.SaveChange();
            }
        }

        public ICollection<AlbumInfoDto> GetAllAlbums()
        {
            var albums = _albumRepository.GetAll();
            return albums.Select(a => a.ToInfoDto()).ToList();
        }

        public AlbumInfoDto? GetAlbumById(int id)
        {
            var album = _albumRepository.GetById(id);
            if (album != null)
            {
                return album?.ToInfoDto();
            }
            return null;
        }
        #endregion
    }
}