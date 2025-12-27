using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using CloudApp.Core.Interfaces;
using CloudApp.Core.Extensions;
using CloudApp.Core.Interfaces.Repositories;

namespace CloudApp.Service
{
    public class AlbumService : IAlbumService
    {
        private readonly IRepository<Album> _albumRepository;

        public AlbumService(IRepository<Album> albumRepository)
        {
            _albumRepository = albumRepository;
        }

        #region 同步方法
        public void AddAlbum(CreateAlbumDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            Album album = model.ToEntity();
            _albumRepository.Add(album);
            _albumRepository.SaveChange();
        }

        public ICollection<AlbumInfoDto> GetAllAlbums()
        {
            var albums = _albumRepository.GetAll();
            return albums.Select(a => a.ToInfoDto()).ToList();
        }

        public AlbumInfoDto GetAlbumById(int id)
        {
            var album = _albumRepository.GetById(id);
            if (album == null)
            {
                throw new ArgumentException(nameof(id), "专辑不存在");
            }
            return album.ToInfoDto();
        }
        
        public void DeleteAlbum(int id)
        {
            bool albumExists = _albumRepository.Exists(id);
            if (!albumExists)
            {
                throw new ArgumentException(nameof(id), "专辑不存在");
            }
            
            _albumRepository.Delete(id);
            _albumRepository.SaveChange();
        }
        
        public void UpdateAlbum(int id, CreateAlbumDto model)
        {
            Album album = _albumRepository.GetById(id);
            if (album != null)
            {
                album = model.ToEntity();
            }
        }
        #endregion
    }
}
