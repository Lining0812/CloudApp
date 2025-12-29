using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;

namespace CloudApp.Core.Extensions
{
    public static class AlbumExtension
    {
        public static Album ToEntity(this CreateAlbumDto dto,string imageurl)
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
                CoverImageUrl = imageurl,

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
                CoverImageUrl = album.CoverImageUrl,

                Tracks = album.Tracks.Select(t => t.Title).ToList(),
            };
        }
    }
}
