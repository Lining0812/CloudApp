using CloudApp.Core.Dtos.Album;
using CloudApp.Core.Entities;

namespace CloudApp.Core.Extensions
{
    public static class AlbumExtension
    {
        public static Album ToEntity(this CreateAlbumRequest dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            return new Album()
            {
                Title = dto.Title,
                Description = dto.Description,
                Artist = dto.Artist,
                ReleaseDate = dto.ReleaseDate,
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
