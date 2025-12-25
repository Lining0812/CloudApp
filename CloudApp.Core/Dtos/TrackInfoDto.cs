using CloudApp.Core.Entities;

namespace CloudApp.Core.Dtos
{
    public class TrackInfoDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Composer { get; set; }
        public string Lyricist { get; set; }
    }
}
