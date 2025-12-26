using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using CloudApp.Core.Extensions;
using CloudApp.Core.Interfaces;
using CloudApp.Data.Repositories;

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

        #region 查询方法
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
                throw new ArgumentException("曲目不存在");
            }
            return track.ToInfoDto();
        }
        #endregion

        #region 操作方法
        public void AddTrack(CreateTrackDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Album album = _albumrepository.GetById(model.AlbumId);
            if (album == null)
            {
                throw new ArgumentException("无效的专辑ID");
            }

            var track = model.ToEntity();

            _trackrepository.Add(track);
        }

        public void UpdateTrack(int id, CreateTrackDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var track = _trackrepository.GetById(id);
            if (track == null)
            {
                throw new ArgumentException("曲目不存在");
            }

            Album album = _albumrepository.GetById(model.AlbumId);
            if (album == null)
            {
                throw new ArgumentException("无效的专辑ID");
            }

            track.Title = model.Title;
            track.Subtitle = model.Subtitle;
            track.Description = model.Description;
            track.URL = model.URL;
            track.Artist = model.Artist;
            track.Composer = model.Composer;
            track.Lyricist = model.Lyricist;
            track.Album = album;
            track.AlbumId = model.AlbumId;

            _trackrepository.Update(track);
        }

        public void DeleteTrack(int id)
        {
            if (!_trackrepository.Exists(id))
            {
                throw new ArgumentException("曲目不存在");
            }

            _trackrepository.Delete(id);
        }
        #endregion 
    }
}
