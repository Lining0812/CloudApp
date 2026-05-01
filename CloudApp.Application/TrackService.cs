using CloudApp.Core.Dtos.Track;
using CloudApp.Core.Entities;
using CloudApp.Core.Exceptions;
using CloudApp.Core.Extensions;
using CloudApp.Core.Interfaces.Repositories;
using CloudApp.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace CloudApp.Application
{
    public class TrackService : ITrackService
    {
        private readonly ITrackRepository _trackRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IConcertRepository _concertRepository;
        private readonly ILogger<TrackService> _logger;

        public TrackService(
            ITrackRepository trackRepository,
            IAlbumRepository albumRepository,
            IConcertRepository concertRepository,
            ILogger<TrackService> logger)
        {
            _trackRepository = trackRepository;
            _albumRepository = albumRepository;
            _concertRepository = concertRepository;
            _logger = logger;
        }

        #region 同步方法
        public void CreateTrack(CreateTrackDto model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            // 检查同名单曲是否已存在
            var exists = _trackRepository.TrackExists(model.Title);
            if (exists) throw new InvalidOperationException($"单曲 '{model.Title}' 已存在");

            // 验证关联专辑是否存在
            if (model.AlbumId.HasValue)
            {
                bool albumExists = _albumRepository.Exists(model.AlbumId.Value);
                if (!albumExists) throw new EntityNotFoundException("专辑", model.AlbumId.Value);
            }

            // 验证关联演唱会是否存在
            if (model.ConcertId.HasValue)
            {
                bool concertExists = _concertRepository.Exists(model.ConcertId.Value);
                if (!concertExists) throw new EntityNotFoundException("演唱会", model.ConcertId.Value);
            }

            Track track = model.ToEntity();
            _trackRepository.Add(track);
            _trackRepository.SaveChange();

            _logger.LogInformation("成功添加单曲: ID={TrackId}, Title={Title}", track.Id, track.Title);
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
                bool trackExists = _trackRepository.Exists(id);
                if (!trackExists)
                {
                    _logger.LogWarning("尝试删除不存在的单曲: ID={TrackId}", id);
                    throw new ArgumentException("单曲不存在", nameof(id));
                }
                _trackRepository.Delete(id);
                _trackRepository.SaveChange();
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
                var track = _trackRepository.GetById(id);
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

                _trackRepository.Update(track);
                _trackRepository.SaveChange();
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
                var tracks = _trackRepository.GetAll();
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
                var track = _trackRepository.GetById(id);
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
