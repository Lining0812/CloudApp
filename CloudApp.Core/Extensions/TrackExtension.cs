using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;

namespace CloudApp.Core.Extensions
{
    public static class TrackExtension
    {
        public static Track ToEntity(this CreateTrackDto dto, string imageurl)
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
                Artist = dto.Artist,
                Composer = dto.Composer,
                Lyricist = dto.Lyricist,
                CoverImageUrl = imageurl,
                AlbumId = dto.AlbumId,
                ConcertId = dto.ConcertId,

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
                Artist = track.Artist,
                Composer = track.Composer,
                Lyricist = track.Lyricist,
                CoverImageUrl = track.CoverImageUrl,
                AlbumTitle = track.Album?.Title,
                ConcertTitle = track.Concert?.Title,
            };
        }
    }
}
