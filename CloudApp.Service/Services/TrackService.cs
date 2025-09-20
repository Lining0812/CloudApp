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
            var album = _albumrepository.GetEntityById(model.AlbumId);

            if (album != null)
            {
                var track = new Track
                {
                    Title = model.Title,
                    Album = album
                };
                _trackrepository.AddEntity(track);
            }
        }

        public IEnumerable<TrackInfoDto> GetAllTracks()
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
