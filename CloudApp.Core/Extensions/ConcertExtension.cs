using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudApp.Core.Extensions
{
    public static class ConcertExtension
    {
        public static Concert ToEntity(this CreateConcertDto dto)
        {
            return new Concert
            {
                Title = dto.Title,
                Description = dto.Description,
                StartAt = dto.StartAt,
                EndAt = dto.EndAt,
                Location = dto.Location
            };
        }
    }
}
