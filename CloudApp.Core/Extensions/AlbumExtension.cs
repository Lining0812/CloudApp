using CloudApp.Core.Dtos;
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
                CoverImageUrl = dto.CoverImageUrl,

                CreatedAt = now,
                UpdatedAt = now,
                IsDeleted = false,
            };
        }
    }
}
