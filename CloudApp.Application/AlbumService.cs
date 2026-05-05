using CloudApp.Core.Dtos.Album;
using CloudApp.Core.Entities;
using CloudApp.Core.Exceptions;
using CloudApp.Core.Extensions;
using CloudApp.Core.Interfaces.Repositories;
using CloudApp.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace CloudApp.Application
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly ILogger<AlbumService> _logger;

        public AlbumService(
            IAlbumRepository repository,
            ILogger<AlbumService> logger)
        {
            _albumRepository = repository;
            _logger = logger;
        }

        #region 同步方法
        public void CreateAlbum(CreateAlbumRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            _logger.LogInformation("开始添加专辑: {Title}, 艺术家: {Artist}", request.Title, request.Artist);

            if (_albumRepository.AlbumExists(request.Title))
                throw new BusinessException($"专辑《{request.Title}》已存在");

            Album model = request.ToEntity();
            _albumRepository.Add(model);
            _albumRepository.SaveChange();

            _logger.LogInformation("成功添加专辑: ID={Id}, Title={Title}", model.Id, model.Title);
        }

        public void DeleteAlbum(int id)
        {
            if (id <= 0)
                throw new ArgumentException("专辑ID无效", nameof(id));

            _logger.LogInformation("开始删除专辑: ID={AlbumId}", id);

            var album = _albumRepository.GetById(id);
            if (album == null)
                throw new EntityNotFoundException("专辑", id);

            if (album.Tracks.Any(t => !t.IsDeleted))
                throw new BusinessException("专辑下存在未删除的单曲，无法删除");

            album.Delete();
            _albumRepository.SaveChange();

            _logger.LogInformation($"成功删除专辑: ID={id}");
        }

        public void UpdateAlbum(int id, CreateAlbumRequest model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (id <= 0)
                throw new BusinessException("专辑ID无效");

            _logger.LogInformation("开始更新专辑: ID={AlbumId}", id);
            var album = _albumRepository.GetById(id);
            if (album == null)
                throw new EntityNotFoundException("专辑", id);

            // 检查新标题是否与其他专辑冲突
            var existing = _albumRepository.FindAlbumByTitle(model.Title);
            if (existing != null && existing.Id != id)
                throw new BusinessException($"专辑 '{model.Title}' 已存在");

            album.Title = model.Title;
            album.Description = model.Description;
            album.Artist = model.Artist;
            album.ReleaseDate = model.ReleaseDate;
            album.UpdatedAt = DateTime.UtcNow;

            _albumRepository.Update(album);
            _albumRepository.SaveChange();

            _logger.LogInformation("成功更新专辑: ID={AlbumId}, Title={Title}", album.Id, album.Title);
        }

        public ICollection<AlbumInfoDto> GetAllAlbums()
        {
            return _albumRepository.GetAllAsDto().ToList();
        }
        #endregion
    }
}