using CloudApp.Core.Entities;
using CloudApp.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudApp.Service.Services
{
    public class TrackService
    {
        private readonly IRepository<Track> _trackrepository;
        private readonly IRepository<Album> _albumrepository;
        public TrackService(IRepository<Album> albumrepository,IRepository<Track> trackrepository)
        {
            _albumrepository = albumrepository;
            _trackrepository = trackrepository;
        }

        public void AddTrack(CreatTrackDto model)
        {
            if (_albumrepository.Find(model.AlbumId))
            {
                var track = new Track
                {
                    Title = model.Title,
                    Album = _albumrepository.GetById(model.AlbumId)
                };
                _trackrepository.Add(track);
            }
        }
    }

    public class CreatTrackDto
    {
        public string Title { get; set; }
        public int AlbumId { get; set; }
    }
}
