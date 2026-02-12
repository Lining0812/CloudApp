using CloudApp.Core.Dtos.Track;
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
                AlbumTitle = track.Album?.Title,
                ConcertTitle = track.Concert?.Title,
            };
        }

    }
}
