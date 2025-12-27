using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;

namespace CloudApp.Core.Extensions
{
    public static class TrackExtension
    {
        public static Track ToEntity(this CreateTrackDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            var now = DateTime.UtcNow;
            return new Track()
            {
                Title = dto.Title,
                Subtitle = dto.Subtitle,
                Description = dto.Description,
                Duration = dto.Duration,
                ReleaseDate = dto.ReleaseDate,
                CoverImageUrl = dto.CoverImageUrl,
                Artist = dto.Artist,
                Composer = dto.Composer,
                Lyricist = dto.Lyricist,
                AlbumId = dto.AlbumId,

                CreatedAt = now,
                UpdatedAt = now,
                IsDeleted = false,
            };
        }

        public static TrackInfoDto ToInfoDto(this Track track)
        {
            return new TrackInfoDto()
            {
                Title = track.Title + track.Subtitle,
                Description = track.Description,
                Duration = track.Duration,
                Composer = track.Composer,
                Lyricist = track.Lyricist,
            };
        }
    }
}
