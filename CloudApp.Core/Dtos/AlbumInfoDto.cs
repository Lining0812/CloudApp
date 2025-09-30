using CloudApp.Core.Entities;

namespace CloudApp.Core.Dtos
{
    public class AlbumInfoDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Artist { get; set; }

        public DateTime ReleaseDate { get; set; }

        public List<string> Tracks { get; set; }

        private AlbumInfoDto()
        {
        }

        public AlbumInfoDto(Album album)
        {
            Title = album.Title;
            Description = album.Description;
            Artist = album.Artist;
            ReleaseDate = album.ReleaseDate;
            Tracks = album.Tracks.Select(t => t.Title).ToList();
        }
    }
}
