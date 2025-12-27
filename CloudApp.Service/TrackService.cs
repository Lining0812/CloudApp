using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using CloudApp.Core.Extensions;
using CloudApp.Core.Interfaces;
using CloudApp.Core.Interfaces.Repositories;

namespace CloudApp.Service
{
    public class TrackService : ITrackService
    {
        private readonly IRepository<Track> _trackrepository;
        private readonly IRepository<Album> _albumrepository;

        public TrackService(IRepository<Album> albumrepository, IRepository<Track> trackrepository)
        {
            _albumrepository = albumrepository;
            _trackrepository = trackrepository;
        }

        #region 同步方法
        public ICollection<TrackInfoDto> GetAllTracks()
        {
            var tracks = _trackrepository.GetAll();
            return tracks.Select(t => t.ToInfoDto()).ToList();
        }
        public TrackInfoDto GetTrackById(int id)
        {
            var track = _trackrepository.GetById(id);
            if (track == null)
            {
                throw new ArgumentException("��Ŀ������");
            }
            return track.ToInfoDto();
        }

        public void AddTrack(CreateTrackDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Album album = _albumrepository.GetById(model.AlbumId);
            if (album == null)
            {
                throw new ArgumentException("��Ч��ר��ID");
            }

            var track = model.ToEntity();

            _trackrepository.Add(track);
            _trackrepository.SaveChange();
        }

        public void UpdateTrack(int id, CreateTrackDto model)
        {
        }

        public void DeleteTrack(int id)
        {
            if (!_trackrepository.Exists(id))
            {
                throw new ArgumentException("��Ŀ������");
            }

            _trackrepository.Delete(id);
        }
        #endregion 
    }
}
