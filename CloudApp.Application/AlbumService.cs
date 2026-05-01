using CloudApp.Core.Dtos.Album;
using CloudApp.Core.Entities;
using CloudApp.Core.Enums;
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

        private readonly Entype _type = Entype.Album;

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
            {
                _logger.LogWarning("尝试添加专辑时，模型为null");
                throw new ArgumentNullException(nameof(request));
            }

            try
            {
                _logger.LogInformation($"开始添加专辑: {request.Title}, 艺术家: {request.Artist}");

                var album = _albumRepository.FindAlbumByTitle(request.Title);

                if (album != null)
                {
                    _logger.LogWarning($"尝试添加已存在的专辑: Title={request.Title}");
                    throw new BusinessException("专辑已存在");
                }

                var model = request.ToEntity();
                _albumRepository.Add(model);
                _albumRepository.SaveChange();

                _logger.LogInformation($"成功添加专辑: ID={model.Id}, Title={model.Title}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"添加专辑失败: Title={request?.Title}");
                throw;
            }
        }

        public void DeleteAlbum(int id)
        {
            if (id <= 0) throw new BusinessException("专辑ID无效");

            var album = _albumRepository.GetById(id);
            if (album == null) 
                throw new EntityNotFoundException("专辑", id);

            if (album.Tracks.Any(t => !t.IsDeleted))
                throw new BusinessException("专辑下存在未删除的单曲，无法删除");

            // 删除专辑（软删除）
            album.Delete();
            _albumRepository.SaveChange();
        }

        public void UpdateAlbum(int id, CreateAlbumRequest model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (id <= 0) throw new BusinessException("专辑ID无效");

            var album = _albumRepository.GetById(id);
            if (album == null) throw new EntityNotFoundException("专辑", id);

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
        }

        public ICollection<AlbumInfoDto> GetAllAlbums()
        {
            return _albumRepository.GetAllAsDto().ToList();
        }
        #endregion
    }
}