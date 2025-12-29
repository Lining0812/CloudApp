
using Microsoft.AspNetCore.Http;

namespace CloudApp.Core.Dtos
{
    public class AlbumInfoDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Artist { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<string> Tracks { get; set; }
    }
}
