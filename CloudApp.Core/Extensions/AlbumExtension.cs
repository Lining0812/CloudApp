using CloudApp.Core.Dtos.Album;
using CloudApp.Core.Entities;

namespace CloudApp.Core.Extensions
{
    public static class AlbumExtension
    {
        public static Album ToEntity(this CreateAlbumDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            var now = DateTime.UtcNow;
            return new Album()
            {
                Title = dto.Title,
                Description = dto.Description,
                Artist = dto.Artist,
                ReleaseDate = dto.ReleaseDate,

                CreatedAt = now,
                UpdatedAt = now,
                IsDeleted = false,
            };
        }

        public static AlbumInfoDto ToInfoDto(this Album album)
        {
            if (album == null)
            {
                throw new ArgumentNullException(nameof(album));
            }
            return new AlbumInfoDto()
            {
                Id = album.Id,
                Title = album.Title,
                Description = album.Description,
                Artist = album.Artist,
                ReleaseDate = album.ReleaseDate,

                Tracks = album.Tracks.Select(t => t.Title).ToList(),
            };
        }
    }
}
