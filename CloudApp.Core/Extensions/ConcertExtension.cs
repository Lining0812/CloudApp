using CloudApp.Core.Dtos.Concert;
using CloudApp.Core.Entities;

namespace CloudApp.Core.Extensions
{
    public static class ConcertExtension
    {
        /// <summary>
        /// Dto转换为实体类
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="imageurl"></param>
        /// <returns></returns>
        public static Concert ToEntity(this CreateConcertDto dto, string imageurl)
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

        /// <summary>
        /// Dto转换为实体类
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

                Tracks = concert.Tracks.Select(t=>t.Title).ToArray(),
            };
        }

        /// <summary>
        /// 实体类更新
        /// </summary>
        /// <param name="concert"></param>
        /// <param name="newconcert"></param>
        /// <returns></returns>
        //public static Concert Update(this Concert concert, Concert newconcert)
        //{
        //}
    }
}
