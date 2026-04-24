using CloudApp.Core.Entities;
using CloudApp.Core.Extensions;
using CloudApp.Core.Interfaces.Services;
using CloudApp.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using CloudApp.Core.Enums;
using CloudApp.Core.Dtos.Track;
using CloudApp.Core.Dtos.Album;

namespace CloudApp.Application
{
    public class TrackService : ITrackService
    {
        private readonly ITrackRepository _trackrepository;
        private readonly IAlbumService _albumService;
        private readonly IStorageProvider _storageProvider;
        private readonly ILogger<TrackService> _logger;
        private readonly Entype _type = Entype.Track;

        public TrackService(
            ITrackRepository trackrepository,
            IAlbumService albumService, 
            ILogger<TrackService> logger, 
            IStorageProvider storageProvider)
        {
            _trackrepository = trackrepository;
            _albumService = albumService;
            _logger = logger;
            _storageProvider = storageProvider;
        }

        #region 同步方法
        public void CreateTrack(CreateTrackDto model)
        {
            if (model == null)
            {
                _logger.LogWarning("尝试添加单曲时，模型为null");
                throw new ArgumentNullException(nameof(model));
            }

            string coverImageUrl = string.Empty;
            try
            {
                _logger.LogInformation("开始添加单曲: {Title}, 艺术家: {Artist}", model.Title, model.Artist);

                // 处理封面图片上传
                if (model.CoverImage != null && model.CoverImage.Length > 0)
                {
                    _logger.LogInformation("开始上传单曲封面图片: FileName={FileName}, Size={Size} bytes",
                        model.CoverImage.FileName, model.CoverImage.Length);

                    coverImageUrl = _storageProvider.SaveFile(model.CoverImage, _type);

                    _logger.LogInformation("封面图片上传成功: Path={CoverImageUrl}", coverImageUrl);
                }
                else
                {
                    _logger.LogWarning("添加单曲时未提供封面图片");
                }

                // 检查是否需要自动创建专辑
                if (!model.AlbumId.HasValue)
                {
                    _logger.LogInformation("未指定专辑ID，准备自动创建专辑");
                    
                    // 创建与单曲同名的专辑
                    var creatAlbum = new CreateAlbumRequest
                    {
                        Title = $"{model.Title}-(Single)",
                        Artist = model.Artist,
                        ReleaseDate = model.ReleaseDate
                    };
                    
                    _albumService.CreateAlbum(creatAlbum);
                    
                    _logger.LogInformation($"自动创建专辑成功:Title={creatAlbum.Title}");
                    
                }

                // 创建单曲实体并保存
                Track track = model.ToEntity(coverImageUrl);
                _trackrepository.Add(track);
                _trackrepository.SaveChange();
                _logger.LogInformation("成功添加单曲: ID={TrackId}, Title={Title}, AlbumId={AlbumId}", track.Id, track.Title, track.AlbumId);
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
                _logger.LogError(ex, "添加单曲失败: Title={Title}, Artist={Artist}", model.Title, model.Artist);
                throw;
            }
        }
        public void DeleteTrack(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("尝试删除单曲时，ID无效: {TrackId}", id);
                throw new ArgumentException("单曲ID无效", nameof(id));
            }

            try
            {
                _logger.LogInformation("开始删除单曲: ID={TrackId}", id);
                bool trackExists = _trackrepository.Exists(id);
                if (!trackExists)
                {
                    _logger.LogWarning("尝试删除不存在的单曲: ID={TrackId}", id);
                    throw new ArgumentException("单曲不存在", nameof(id));
                }
                _trackrepository.Delete(id);
                _trackrepository.SaveChange();
                _logger.LogInformation("成功删除单曲: ID={TrackId}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除单曲失败: ID={TrackId}", id);
                throw;
            }
        }
        public void UpdateTrack(int id, CreateTrackDto model)
        {
            if (model == null)
            {
                _logger.LogWarning("尝试更新单曲时，模型为null: ID={TrackId}", id);
                throw new ArgumentNullException(nameof(model));
            }

            try
            {
                _logger.LogInformation("开始更新单曲: ID={TrackId}", id);
                var track = _trackrepository.GetById(id);
                if (track == null)
                {
                    _logger.LogWarning("尝试更新不存在的单曲: ID={TrackId}", id);
                    throw new ArgumentException("单曲不存在", nameof(id));
                }

                track.Title = model.Title;
                track.Duration = model.Duration;
                track.Subtitle = model.Subtitle;
                track.Description = model.Description;
                track.ReleaseDate = model.ReleaseDate;
                track.Artist = model.Artist;
                track.Composer = model.Composer;
                track.Lyricist = model.Lyricist;
                track.UpdatedAt = DateTime.UtcNow;

                _trackrepository.Update(track);
                _trackrepository.SaveChange();
                _logger.LogInformation("成功更新单曲: ID={TrackId}, Title={Title}", track.Id, track.Title);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新单曲失败: ID={TrackId}", id);
                throw;
            }
        }
        public ICollection<TrackInfoDto> GetAllTracks()
        {
            try
            {
                _logger.LogDebug("开始获取所有单曲列表");
                var tracks = _trackrepository.GetAll();
                var result = tracks.Select(t => t.ToInfoDto()).ToList();
                _logger.LogInformation("成功获取单曲列表，共 {Count} 条记录", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取单曲列表失败");
                throw;
            }
        }
        public TrackInfoDto GetById(int id)
        {
            try
            {
                _logger.LogDebug("开始获取单曲详情: ID={TrackId}", id);
                var track = _trackrepository.GetById(id);
                if (track == null)
                {
                    _logger.LogWarning("未找到单曲: ID={TrackId}", id);
                    throw new ArgumentException("单曲不存在", nameof(id));
                }
                _logger.LogInformation("成功获取单曲详情: ID={TrackId}, Title={Title}", track.Id, track.Title);
                return track.ToInfoDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取单曲详情失败: ID={TrackId}", id);
                throw;
            }
        }
        public ICollection<Track> GetByAlbumdID()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
