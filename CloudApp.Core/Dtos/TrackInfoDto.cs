using CloudApp.Core.Entities;

namespace CloudApp.Core.Dtos
{
    public class TrackInfoDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public TimeSpan Duration { get; set; }
        public string Artist { get; set; }
        public string Composer { get; set; }
        public string Lyricist { get; set; }
        public string CoverImageUrl { get; set; }
        public string? AlbumTitle { get; set; }
        public string? ConcertTitle { get; set; }
    }
}
