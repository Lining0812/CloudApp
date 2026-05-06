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
        private readonly ILogger<TrackService> _logger;

        public TrackService(
            ITrackRepository trackRepository,
            IAlbumRepository albumRepository,
            ILogger<TrackService> logger)
        {
            _trackRepository = trackRepository;
            _albumRepository = albumRepository;
            _logger = logger;
        }

        #region 同步方法
        public void CreateTrack(TrackCreateDto model)
        {
            if (model == null) throw new BusinessException(nameof(model));

            // 验证Album是否存在
            if (model.AlbumId.HasValue)
            {
                bool albumExists = _albumRepository.Exists(model.AlbumId.Value);
                if (!albumExists)
                    throw new EntityNotFoundException("专辑", model.AlbumId.Value);
            }

            // 检查同名单曲是否已存在
            var exists = _trackRepository.TrackExists(model.Title, model.AlbumId);
            if (exists)
                throw new BusinessException($"单曲《{model.Title}》已存在");

            Track track = model.ToEntity();
            _trackRepository.Add(track);
            _trackRepository.SaveChange();

            _logger.LogInformation($"成功添加单曲: ID={track.Id}, Title={track.Title}");
        }

        public void DeleteTrack(int id)
        {
            if (id <= 0)
                throw new ArgumentException("单曲ID无效", nameof(id));

            var track = _trackRepository.GetById(id);
            if (track == null)
                throw new EntityNotFoundException("单曲", id);

            track.Delete();
            _trackRepository.SaveChange();

            _logger.LogInformation("成功删除单曲: ID={Id}", id);
        }

        public void UpdateTrack(int id, TrackCreateDto model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var track = _trackRepository.GetById(id);
            if (track == null)
                throw new ArgumentException("单曲不存在", nameof(id));

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
        public ICollection<TrackInfoDto> GetAllTracks()
        {
            var tracks = _trackRepository.GetAll();
            var result = tracks.Select(t => t.ToInfoDto()).ToList();
            _logger.LogInformation("成功获取单曲列表，共 {Count} 条记录", result.Count);
            return result;
        }
        public TrackInfoDto GetById(int id)
        {
            var track = _trackRepository.GetById(id);
            if (track == null)
                throw new ArgumentException("单曲不存在", nameof(id));

            _logger.LogInformation("成功获取单曲详情: ID={TrackId}, Title={Title}", track.Id, track.Title);
            return track.ToInfoDto();
        }
        public ICollection<Track> GetByAlbumdID()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
