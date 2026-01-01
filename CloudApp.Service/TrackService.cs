using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using CloudApp.Core.Extensions;
using CloudApp.Core.Interfaces.Services;
using CloudApp.Core.Interfaces.Repositories;

namespace CloudApp.Service
{
    public class TrackService : ITrackService
    {
        private readonly ITrackRepository _trackrepository;
        private readonly IImageStorageService _imagestorageservice;

        public TrackService(ITrackRepository trackrepository, IImageStorageService imagestorageservice)
        {
            _trackrepository = trackrepository;
            _imagestorageservice = imagestorageservice;
        }

        #region 同步方法
        public void AddTrack(CreateTrackDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            //Album album = _albumrepository.GetById(model.AlbumId);
            //Concert concert = _concertrepository.GetById(model.ConcertId);
            //if (album == null)
            //{
            //    throw new ArgumentException("专辑不存在");
            //}
            string url = _imagestorageservice.SaveTrackImage(model.CoverImage);
            var track = model.ToEntity(url);
            _trackrepository.Add(track);
            _trackrepository.SaveChange();
        }
        public void DeleteTrack(int id)
        {
            bool trackExists = _trackrepository.Exists(id);
            if (!trackExists)
            {
                throw new ArgumentException(nameof(id), "单曲不存在");
            }
            _trackrepository.Delete(id);
            _trackrepository.SaveChange();
        }
        public void UpdateTrack(int id, CreateTrackDto model)
        {
            var track = _trackrepository.GetById(id);
            if (track == null)
            {
                throw new ArgumentException(nameof(id), "单曲不存在");
            }
            else
            {
                track.Title = model.Title;
                track.Duration = model.Duration;
                track.Subtitle = model.Subtitle;
                track.Description = model.Description;
                track.ReleaseDate = model.ReleaseDate;
                track.Artist = model.Artist;
                track.Composer = model.Composer;
                track.Lyricist = model.Lyricist;
                track.UpdatedAt = DateTime.UtcNow;

                _trackrepository.Update(track);
                _trackrepository.SaveChange();
            }
        }

        public ICollection<TrackInfoDto> GetAllTracks()
        {
            var tracks = _trackrepository.GetAll();
            return tracks.Select(t => t.ToInfoDto()).ToList();
        }

        public TrackInfoDto GetById(int id)
        {
            var track = _trackrepository.GetById(id);
            if (track == null)
            {
                throw new ArgumentException(nameof(id), "单曲不存在");
            }
            return track.ToInfoDto();
        }

        public ICollection<Track> GetByAlbumdID()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
