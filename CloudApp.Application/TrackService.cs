using CloudApp.Core.Dtos.Track;
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

        #region 写入

        public async Task<TrackInfoDto> CreateTrackAsync(TrackCreateDto dto, CancellationToken ct = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            ValidateCreate(dto);

            // 专辑存在性校验（AlbumId 为空表示暂不归属专辑）
            await EnsureAlbumExistsAsync(dto.AlbumId, ct);

            var title = dto.Title.Trim();

            // 同专辑内不允许重名
            if (await _trackRepository.TrackExistsAsync(title, dto.AlbumId, null, ct))
                throw new BusinessException($"单曲《{title}》已存在");

            var track = dto.ToEntity();
            track.Title = title;
            track.Artist = dto.Artist.Trim();
            track.Composer = dto.Composer.Trim();
            track.Lyricist = dto.Lyricist.Trim();

            await _trackRepository.AddAsync(track);
            await _trackRepository.SaveChangeAsync();

            _logger.LogInformation("成功添加单曲: ID={TrackId}, Title={Title}", track.Id, track.Title);

            // 重新读取一次，让返回的 DTO 带上 AlbumTitle
            var created = await _trackRepository.GetByIdAsync(track.Id) ?? track;
            return created.ToInfoDto();
        }

        public async Task<TrackInfoDto> UpdateTrackAsync(int id, TrackUpdateDto model, CancellationToken ct = default)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (id <= 0) throw new BusinessException("单曲ID无效");

            var track = await _trackRepository.GetByIdAsync(id)
                ?? throw new EntityNotFoundException("单曲", id);

            // 1) 专辑变更：ClearAlbumId 优先（PATCH 语义下 AlbumId=null 只能表示“不修改”）
            if (model.ClearAlbumId)
            {
                track.AlbumId = null;
            }
            else if (model.AlbumId.HasValue && model.AlbumId.Value != track.AlbumId)
            {
                await EnsureAlbumExistsAsync(model.AlbumId, ct);
                track.AlbumId = model.AlbumId.Value;
            }

            // 2) 标题变更需要重新做同重校验（排除自身）
            if (!string.IsNullOrWhiteSpace(model.Title))
            {
                var title = model.Title.Trim();
                if (!string.Equals(title, track.Title, StringComparison.Ordinal))
                {
                    if (await _trackRepository.TrackExistsAsync(title, track.AlbumId, id, ct))
                        throw new BusinessException($"单曲《{title}》已存在");
                }
            }

            // 3) 其余字段：DTO 中提供了才覆盖
            if (model.Duration.HasValue && model.Duration.Value <= TimeSpan.Zero)
                throw new BusinessException("时长必须大于 0");
            if (model.ReleaseDate.HasValue && model.ReleaseDate.Value == default)
                throw new BusinessException("发行日期不能为空");

            model.ApplyTo(track);

            await _trackRepository.UpdateAsync(track);
            await _trackRepository.SaveChangeAsync();

            _logger.LogInformation("成功更新单曲: ID={TrackId}, Title={Title}", track.Id, track.Title);

            // 专辑可能已变更，重新读取以获得准确的 AlbumTitle
            var updated = await _trackRepository.GetByIdAsync(track.Id) ?? track;
            return updated.ToInfoDto();
        }

        public async Task DeleteTrackAsync(int id, CancellationToken ct = default)
        {
            if (id <= 0) throw new BusinessException("单曲ID无效");

            var track = await _trackRepository.GetByIdAsync(id)
                ?? throw new EntityNotFoundException("单曲", id);

            await _trackRepository.DeleteAsync(track);   // 软删除：置 IsDeleted/DeletedAt
            await _trackRepository.SaveChangeAsync();

            _logger.LogInformation("成功删除单曲: ID={TrackId}, Title={Title}", track.Id, track.Title);
        }

        #endregion

        #region 查询

        public async Task<ICollection<TrackInfoDto>> GetAllTracksAsync(CancellationToken ct = default)
        {
            var tracks = await _trackRepository.GetAllAsync();
            var result = tracks.Select(t => t.ToInfoDto()).ToList();

            _logger.LogInformation("成功获取单曲列表，共 {Count} 条记录", result.Count);
            return result;
        }

        public async Task<TrackInfoDto> GetByIdAsync(int id, CancellationToken ct = default)
        {
            if (id <= 0) throw new BusinessException("单曲ID无效");

            var track = await _trackRepository.GetByIdAsync(id)
                ?? throw new EntityNotFoundException("单曲", id);

            _logger.LogInformation("成功获取单曲详情: ID={TrackId}, Title={Title}", track.Id, track.Title);
            return track.ToInfoDto();
        }

        public async Task<ICollection<TrackInfoDto>> GetTracksByAlbumIdAsync(int albumId, CancellationToken ct = default)
        {
            if (albumId <= 0) throw new BusinessException("专辑ID无效");
            if (!await _albumRepository.ExistsAsync(albumId))
                throw new EntityNotFoundException("专辑", albumId);

            var tracks = await _trackRepository.GetTracksByAlbumIdAsync(albumId, ct);
            var result = tracks.Select(t => t.ToInfoDto()).ToList();

            _logger.LogInformation("成功获取专辑曲目: AlbumId={AlbumId}, 共 {Count} 条记录", albumId, result.Count);
            return result;
        }

        public async Task<ICollection<TrackInfoDto>> SearchTracksAsync(string keyword, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                throw new BusinessException("搜索关键字不能为空");

            var tracks = await _trackRepository.GetTracksByTitleAsync(keyword, ct);
            var result = tracks.Select(t => t.ToInfoDto()).ToList();

            _logger.LogInformation("成功搜索单曲: Keyword={Keyword}, 共 {Count} 条记录", keyword, result.Count);
            return result;
        }

        #endregion

        #region 私有方法

        private static void ValidateCreate(TrackCreateDto model)
        {
            // required 关键字只能挡住“字段缺失”，空字符串/零值需要在这里兜住
            if (string.IsNullOrWhiteSpace(model.Title))
                throw new BusinessException("单曲名称不能为空");
            if (string.IsNullOrWhiteSpace(model.Artist))
                throw new BusinessException("原唱不能为空");
            if (string.IsNullOrWhiteSpace(model.Composer))
                throw new BusinessException("作曲不能为空");
            if (string.IsNullOrWhiteSpace(model.Lyricist))
                throw new BusinessException("作词不能为空");
            if (model.Duration <= TimeSpan.Zero)
                throw new BusinessException("时长必须大于 0");
            if (model.ReleaseDate == default)
                throw new BusinessException("发行日期不能为空");
        }

        private async Task EnsureAlbumExistsAsync(int? albumId, CancellationToken ct)
        {
            if (!albumId.HasValue) return;

            if (!await _albumRepository.ExistsAsync(albumId.Value))
                throw new EntityNotFoundException("专辑", albumId.Value);
        }

        #endregion
    }
}
