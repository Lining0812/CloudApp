using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using CloudApp.Core.Enums;
using CloudApp.Core.Exceptions;
using CloudApp.Core.Extensions;
using CloudApp.Core.Interfaces.Repositories;
using CloudApp.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace CloudApp.Service
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IStorageProvider _storageProvider;
        private readonly ILogger<AlbumService> _logger;
        private readonly Entype _type = Entype.Album;

        public AlbumService(
            IAlbumRepository repository, 
            IStorageProvider storageProvider,
            ILogger<AlbumService> logger)
        {
            _albumRepository = repository;
            _storageProvider = storageProvider;
            _logger = logger;
        }

        #region 同步方法
        public void AddAlbum(CreateAlbumDto model)
        {
            if (model == null)
            {
                _logger.LogWarning("尝试添加专辑时，模型为null");
                throw new ArgumentNullException(nameof(model));
            }

            string coverImageUrl = string.Empty;
            try
            {
                _logger.LogInformation("开始添加专辑: {Title}, 艺术家: {Artist}", model.Title, model.Artist);

                // 处理封面图片上传
                if (model.CoverImage != null && model.CoverImage.Length > 0)
                {
                    _logger.LogInformation("开始上传专辑封面图片: FileName={FileName}, Size={Size} bytes", 
                        model.CoverImage.FileName, model.CoverImage.Length);
                    
                    coverImageUrl = _storageProvider.SaveFile(model.CoverImage, _type);
                    
                    _logger.LogInformation("封面图片上传成功: Path={CoverImageUrl}", coverImageUrl);
                }
                else
                {
                    _logger.LogWarning("添加专辑时未提供封面图片");
                }

                // 创建专辑实体并保存
                Album album = model.ToEntity(coverImageUrl);
                _albumRepository.Add(album);
                _albumRepository.SaveChange();
                
                _logger.LogInformation("成功添加专辑: ID={AlbumId}, Title={Title}", album.Id, album.Title);
            }
            catch (Exception ex)
            {
                // 如果保存数据库失败，尝试删除已上传的图片
                if (!string.IsNullOrEmpty(coverImageUrl))
                {
                    try
                    {
                        _logger.LogWarning("专辑保存失败，尝试删除已上传的图片: {CoverImageUrl}", coverImageUrl);
                        _storageProvider.DeleteFile(coverImageUrl);
                    }
                    catch (Exception deleteEx)
                    {
                        _logger.LogError(deleteEx, "删除已上传的图片失败: {CoverImageUrl}", coverImageUrl);
                    }
                }
                _logger.LogError(ex, "添加专辑失败: Title={Title}, Artist={Artist}", model.Title, model.Artist);
                throw;
            }
        }

        public void DeleteAlbum(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("尝试删除专辑时，ID无效: {AlbumId}", id);
                throw new ArgumentException("专辑ID无效", nameof(id));
            }

            string? coverImageUrl = null;

            try
            {
                _logger.LogInformation("开始删除专辑: ID={AlbumId}", id);
                Album? album = _albumRepository.GetById(id);
                if (album == null)
                {
                    _logger.LogWarning("尝试删除不存在的专辑: ID={AlbumId}", id);
                    throw new EntityNotFoundException("专辑", id);
                }

                // 保存图片路径，用于后续删除
                coverImageUrl = album.CoverImageUrl;

                // 删除专辑（软删除）
                _albumRepository.Delete(id);
                _albumRepository.SaveChange();
                
                _logger.LogInformation("成功删除专辑: ID={AlbumId}", id);

                // 删除关联的封面图片文件
                if (!string.IsNullOrEmpty(coverImageUrl))
                {
                    try
                    {
                        _logger.LogInformation("开始删除专辑封面图片: Path={CoverImageUrl}", coverImageUrl);
                        _storageProvider.DeleteFile(coverImageUrl);
                        _logger.LogInformation("成功删除专辑封面图片: Path={CoverImageUrl}", coverImageUrl);
                    }
                    catch (Exception deleteEx)
                    {
                        // 图片删除失败不影响专辑删除，只记录警告
                        _logger.LogWarning(deleteEx, "删除专辑封面图片失败: Path={CoverImageUrl}", coverImageUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除专辑失败: ID={AlbumId}", id);
                throw;
            }
        }

        public void UpdateAlbum(int id, CreateAlbumDto model)
        {
            if (model == null)
            {
                _logger.LogWarning("尝试更新专辑时，模型为null: ID={AlbumId}", id);
                throw new ArgumentNullException(nameof(model));
            }

            string? newCoverImageUrl = null;
            string? oldCoverImageUrl = null;

            try
            {
                _logger.LogInformation("开始更新专辑: ID={AlbumId}", id);
                Album? album = _albumRepository.GetById(id);
                if (album == null)
                {
                    _logger.LogWarning("尝试更新不存在的专辑: ID={AlbumId}", id);
                    throw new EntityNotFoundException("专辑", id);
                }

                oldCoverImageUrl = album.CoverImageUrl;

                // 处理封面图片更新
                if (model.CoverImage != null && model.CoverImage.Length > 0)
                {
                    _logger.LogInformation("开始更新专辑封面图片: ID={AlbumId}, FileName={FileName}, Size={Size} bytes", 
                        id, model.CoverImage.FileName, model.CoverImage.Length);

                    // 如果存在旧图片，使用更新方法；否则使用保存方法
                    if (!string.IsNullOrEmpty(oldCoverImageUrl))
                    {
                        newCoverImageUrl = _storageProvider.UpdateFile(oldCoverImageUrl, model.CoverImage);
                        _logger.LogInformation("封面图片更新成功: OldPath={OldPath}, NewPath={NewPath}", 
                            oldCoverImageUrl, newCoverImageUrl);
                    }
                    else
                    {
                        newCoverImageUrl = _storageProvider.SaveFile(model.CoverImage, _type);
                        _logger.LogInformation("封面图片上传成功: Path={CoverImageUrl}", newCoverImageUrl);
                    }
                }

                // 更新专辑信息
                album.Title = model.Title;
                album.Description = model.Description;
                album.Artist = model.Artist;
                album.ReleaseDate = model.ReleaseDate;
                album.UpdatedAt = DateTime.UtcNow;

                // 如果上传了新图片，更新图片路径
                if (!string.IsNullOrEmpty(newCoverImageUrl))
                {
                    album.CoverImageUrl = newCoverImageUrl;
                }

                _albumRepository.Update(album);
                _albumRepository.SaveChange();
                
                _logger.LogInformation("成功更新专辑: ID={AlbumId}, Title={Title}, CoverImageUrl={CoverImageUrl}", 
                    album.Id, album.Title, album.CoverImageUrl);
            }
            catch (Exception ex)
            {
                // 如果保存数据库失败，尝试删除新上传的图片
                if (!string.IsNullOrEmpty(newCoverImageUrl) && newCoverImageUrl != oldCoverImageUrl)
                {
                    try
                    {
                        _logger.LogWarning("专辑更新失败，尝试删除新上传的图片: {CoverImageUrl}", newCoverImageUrl);
                        _storageProvider.DeleteFile(newCoverImageUrl);
                    }
                    catch (Exception deleteEx)
                    {
                        _logger.LogError(deleteEx, "删除新上传的图片失败: {CoverImageUrl}", newCoverImageUrl);
                    }
                }

                _logger.LogError(ex, "更新专辑失败: ID={AlbumId}", id);
                throw;
            }
        }

        public ICollection<AlbumInfoDto> GetAllAlbums()
        {
            try
            {
                _logger.LogDebug("开始获取所有专辑列表");
                var albums = _albumRepository.GetAll();
                var result = albums.Select(a => a.ToInfoDto()).ToList();
                _logger.LogInformation("成功获取专辑列表，共 {Count} 条记录", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取专辑列表失败");
                throw;
            }
        }

        public AlbumInfoDto? GetAlbumById(int id)
        {
            try
            {
                _logger.LogDebug("开始获取专辑详情: ID={AlbumId}", id);
                var album = _albumRepository.GetById(id);
                if (album != null)
                {
                    _logger.LogInformation("成功获取专辑详情: ID={AlbumId}, Title={Title}", album.Id, album.Title);
                    return album.ToInfoDto();
                }
                _logger.LogWarning("未找到专辑: ID={AlbumId}", id);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取专辑详情失败: ID={AlbumId}", id);
                throw;
            }
        }
        #endregion
    }
}