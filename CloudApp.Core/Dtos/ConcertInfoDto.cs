using Microsoft.AspNetCore.Http;

namespace CloudApp.Core.Dtos
{
    public class ConcertInfoDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? CoverImageUrl { get; set; }
    }
}
