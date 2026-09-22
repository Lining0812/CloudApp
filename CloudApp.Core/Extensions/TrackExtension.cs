using CloudApp.Core.Dtos.Track;
using CloudApp.Core.Entities;

namespace CloudApp.Core.Extensions
{
    public static class TrackExtension
    {
        public static Track ToEntity(this TrackCreateDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            return new Track
            {
                Title = dto.Title,
                Subtitle = dto.Subtitle,
                Description = dto.Description,
                Duration = dto.Duration,
                ReleaseDate = dto.ReleaseDate,
                Artist = dto.Artist,
                Composer = dto.Composer,
                Lyricist = dto.Lyricist,
                CoverUrl = dto.CoverUrl,
                LinkUrl = dto.LinkUrl,
                Type = dto.Type,
                AlbumId = dto.AlbumId,
            };
        }

        /// <summary>
        /// 把更新请求中“已提供”的字段覆盖到实体上（PATCH 语义：null = 不修改，空串 = 置空）。
        /// 注意：AlbumId 涉及到专辑存在性校验，由服务层单独处理，这里不覆盖。
        /// </summary>
        public static void ApplyTo(this TrackUpdateDto dto, Track track)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            if (track == null)
                throw new ArgumentNullException(nameof(track));

            if (!string.IsNullOrWhiteSpace(dto.Title)) track.Title = dto.Title.Trim();
            if (!string.IsNullOrWhiteSpace(dto.Artist)) track.Artist = dto.Artist.Trim();
            if (!string.IsNullOrWhiteSpace(dto.Composer)) track.Composer = dto.Composer.Trim();
            if (!string.IsNullOrWhiteSpace(dto.Lyricist)) track.Lyricist = dto.Lyricist.Trim();

            if (dto.Subtitle != null) track.Subtitle = Normalize(dto.Subtitle);
            if (dto.Description != null) track.Description = Normalize(dto.Description);
            if (dto.CoverUrl != null) track.CoverUrl = Normalize(dto.CoverUrl);
            if (dto.LinkUrl != null) track.LinkUrl = Normalize(dto.LinkUrl);

            if (dto.Duration.HasValue) track.Duration = dto.Duration.Value;
            if (dto.ReleaseDate.HasValue) track.ReleaseDate = dto.ReleaseDate.Value;
            if (dto.Type.HasValue) track.Type = dto.Type.Value;
        }

        public static TrackInfoDto ToInfoDto(this Track track)
        {
            if (track == null)
            {
                throw new ArgumentNullException(nameof(track));
            }

            return new TrackInfoDto
            {
                Id = track.Id,
                Title = track.Title,
                Subtitle = track.Subtitle,
                Description = track.Description,
                Duration = track.Duration,
                ReleaseDate = track.ReleaseDate,
                Artist = track.Artist,
                Composer = track.Composer,
                Lyricist = track.Lyricist,
                CoverUrl = track.CoverUrl,
                LinkUrl = track.LinkUrl,
                Type = track.Type,
                AlbumId = track.AlbumId,
                AlbumTitle = track.Album?.Title,
            };
        }

        /// <summary>空白字符串归一化为 null，便于用“空串”表达清空</summary>
        private static string? Normalize(string? value)
            => string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}
