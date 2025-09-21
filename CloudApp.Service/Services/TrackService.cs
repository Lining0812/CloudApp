using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using CloudApp.Data.Repository;
using CloudApp.Service.Interfaces;

namespace CloudApp.Service.Services
{
    public class TrackService : ITrackService
    {
        private readonly IRepository<Track> _trackrepository;
        private readonly IRepository<Album> _albumrepository;
        public TrackService(IRepository<Album> albumrepository,IRepository<Track> trackrepository)
        {
            _albumrepository = albumrepository;
            _trackrepository = trackrepository;
        }

        public void AddTrack(CreateTrackDto model)
        {
            Album album = _albumrepository.GetEntityById(model.AlbumId);

            var track = new Track
            {
                Title = model.Title,
                Subtitle = model.Subtitle,
                Description = model.Description,
                ReleaseDate = DateTime.UtcNow,
                URL = model.URL,
                Artist = model.Artist,
                Composer = model.Composer,
                Lyricist = model.Lyricist,
                Album = album
            };

            this._trackrepository.AddEntity(track);
        }

        public ICollection<TrackInfoDto> GetAllTracks()
        {
            var tracks =  _trackrepository.GetAllEntities();
            return tracks.Select(t => new TrackInfoDto
            {
                Id = t.Id,
                Title = t.Title,
                Subtitle = t.Subtitle,
                Description = t.Description,
                Albumtitle = t.Album.Title,
                Composer = t.Composer,
                Lyricist = t.Lyricist
            }).ToList();
        }
    }
}
