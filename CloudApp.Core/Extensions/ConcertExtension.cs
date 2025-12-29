using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace CloudApp.Core.Extensions
{
    public static class ConcertExtension
    {
        public static Concert ToEntity(this CreateConcertDto dto,string imageurl)
        {
            var now = DateTime.Now;
            return new Concert
            {
                Title = dto.Title,
                Description = dto.Description,
                StartAt = dto.StartAt,
                EndAt = dto.EndAt,
                CoverImageUrl = imageurl,
                Address = dto.Address,

                CreatedAt = now,
                UpdatedAt = now,
                IsDeleted = false,
            };
        }

        public static ConcertInfoDto ToInfoDto(this Concert concert)
        {
            if (concert == null)
            {
                throw new ArgumentNullException(nameof(concert));
            }
            return new ConcertInfoDto()
            {
                Title = concert.Title,
                Description = concert.Description,
                CoverImageUrl = concert.CoverImageUrl,
            };
        }
    }
}
