using CloudApp.Core.Dtos.Concert;
using CloudApp.Core.Entities;

namespace CloudApp.Core.Extensions
{
    public static class ConcertExtension
    {
        /// <summary>
        /// Dto类型转换为实体类
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="imageurl"></param>
        /// <returns></returns>
        public static Concert ToEntity(this CreateConcertRequest dto)
        {
            var now = DateTime.Now;
            return new Concert
            {
                Title = dto.Title,
                Description = dto.Description,
                StartAt = dto.StartAt,
                EndAt = dto.EndAt,
                Address = dto.Address,

                UpdatedAt = now,
            };
        }

        /// <summary>
        /// 实体类转换为Dto类型
        /// </summary>
        /// <param name="concert"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
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

                Tracks = concert.Album?.Tracks.Select(t => t.Title).ToArray() ?? Array.Empty<string>(),
            };
        }

    }
}
