using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using CloudApp.Core.Interface;
using CloudApp.Data.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudApp.Service.Services
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
        /// <summary>
        /// 获取所有专辑
        /// </summary>
        /// <returns></returns>
        public ICollection<TrackInfoDto> GetAllTracks()
        {
            var tracks = _trackrepository.GetAll();
            return tracks.Select(t => new TrackInfoDto(t)
            {
                Title = t.Title,
                Description = t.Description,
                Composer = t.Composer,
                Lyricist = t.Lyricist
            }).ToList();
        }

        /// <summary>
        /// 根据id获取专辑信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public TrackInfoDto GetTrackById(int id)
        {
            var track = _trackrepository.GetById(id);
            if (track == null)
            {
                throw new ArgumentException("曲目不存在");
            }

            return new TrackInfoDto(track)
            {
                Title = track.Title,
                Description = track.Description,
                Composer = track.Composer,
                Lyricist = track.Lyricist
            };
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
                Album = album,
                AlbumId = model.AlbumId
            };

            this._trackrepository.Add(track);
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

        // 异步方法（新增）
        //public async Task AddTrackAsync(CreateTrackDto model)
        //{
        //    if (model == null)
        //    {
        //        throw new ArgumentNullException(nameof(model));
        //    }

        //    Album album = await _albumrepository.GetByIdAsync(model.AlbumId);
        //    if (album == null)
        //    {
        //        throw new ArgumentException("无效的专辑ID");
        //    }

        //    var track = new Track
        //    {
        //        Title = model.Title,
        //        Subtitle = model.Subtitle,
        //        Description = model.Description,
        //        ReleaseDate = DateTime.UtcNow,
        //        URL = model.URL,
        //        Artist = model.Artist,
        //        Composer = model.Composer,
        //        Lyricist = model.Lyricist,
        //        Album = album,
        //        AlbumId = model.AlbumId
        //    };

        //    await this._trackrepository.AddAsync(track);
        //}

        //public async Task<ICollection<TrackInfoDto>> GetAllTracksAsync()
        //{
        //    var tracks = await _trackrepository.GetAllAsync();
        //    return tracks.Select(t => new TrackInfoDto(t)
        //    {
        //        Title = t.Title,
        //        Subtitle = t.Subtitle,
        //        Description = t.Description,
        //        Albumtitle = t.Album?.Title,
        //        Composer = t.Composer,
        //        Lyricist = t.Lyricist
        //    }).ToList();
        //}

        //public async Task<TrackInfoDto> GetTrackByIdAsync(int id)
        //{
        //    var track = await _trackrepository.GetByIdAsync(id);
        //    if (track == null)
        //    {
        //        throw new ArgumentException("曲目不存在");
        //    }

        //    return new TrackInfoDto(track)
        //    {
        //        Title = track.Title,
        //        Subtitle = track.Subtitle,
        //        Description = track.Description,
        //        Albumtitle = track.Album?.Title,
        //        Composer = track.Composer,
        //        Lyricist = track.Lyricist
        //    };
        //}

        //public async Task UpdateTrackAsync(int id, CreateTrackDto model)
        //{
        //    if (model == null)
        //    {
        //        throw new ArgumentNullException(nameof(model));
        //    }

        //    var track = await _trackrepository.GetByIdAsync(id);
        //    if (track == null)
        //    {
        //        throw new ArgumentException("曲目不存在");
        //    }

        //    Album album = await _albumrepository.GetByIdAsync(model.AlbumId);
        //    if (album == null)
        //    {
        //        throw new ArgumentException("无效的专辑ID");
        //    }

        //    track.Title = model.Title;
        //    track.Subtitle = model.Subtitle;
        //    track.Description = model.Description;
        //    track.URL = model.URL;
        //    track.Artist = model.Artist;
        //    track.Composer = model.Composer;
        //    track.Lyricist = model.Lyricist;
        //    track.Album = album;
        //    track.AlbumId = model.AlbumId;

        //    await _trackrepository.UpdateAsync(track);
        //}

        //public async Task DeleteTrackAsync(int id)
        //{
        //    if (!await _trackrepository.ExistsAsync(id))
        //    {
        //        throw new ArgumentException("曲目不存在");
        //    }

        //    await _trackrepository.DeleteAsync(id);
        //}

        #region 扩展功能
        public ICollection<TrackInfoDto> GetTracksByAlbumId(int albumId)
        {
            if (_trackrepository is TrackRepository trackRepo)
            {
                return trackRepo.GetTracksByAlbumId(albumId)
                    .Select(t => new TrackInfoDto(t)
                    {
                        Title = t.Title,
                        Description = t.Description,
                        Composer = t.Composer,
                        Lyricist = t.Lyricist
                    })
                    .ToList();
            }
            return new List<TrackInfoDto>();
            // 降级处理
            //var tracks = _trackrepository.GetByCondition(t => t.AlbumId == albumId);
            //return tracks.Select(t => new TrackInfoDto(t)
            //{
            //    Title = t.Title,
            //    Subtitle = t.Subtitle,
            //    Description = t.Description,
            //    Albumtitle = t.Album?.Title,
            //    Composer = t.Composer,
            //    Lyricist = t.Lyricist
            //}).ToList();
        }

        //public async Task<ICollection<TrackInfoDto>> GetTracksByAlbumIdAsync(int albumId)
        //{
        //    if (_trackrepository is TrackRepository trackRepo)
        //    {
        //        var result = await trackRepo.GetTracksByAlbumIdAsync(albumId);
        //        return result.Select(t => new TrackInfoDto(t)
        //        {
        //            Title = t.Title,
        //            Subtitle = t.Subtitle,
        //            Description = t.Description,
        //            Albumtitle = t.Album?.Title,
        //            Composer = t.Composer,
        //            Lyricist = t.Lyricist
        //        })
        //        .ToList();
        //    }
        //    // 降级处理
        //    var tracks = await _trackrepository.GetByConditionAsync(t => t.AlbumId == albumId);
        //    return tracks.Select(t => new TrackInfoDto(t)
        //    {
        //        Title = t.Title,
        //        Subtitle = t.Subtitle,
        //        Description = t.Description,
        //        Albumtitle = t.Album?.Title,
        //        Composer = t.Composer,
        //        Lyricist = t.Lyricist
        //    }).ToList();
        //}

        //public ICollection<TrackInfoDto> GetTracksWithPagination(int pageNumber, int pageSize)
        //{
        //    var tracks = _trackrepository.GetPaged(pageNumber, pageSize);
        //    return tracks.Select(t => new TrackInfoDto(t)
        //    {
        //        Title = t.Title,
        //        Subtitle = t.Subtitle,
        //        Description = t.Description,
        //        Albumtitle = t.Album?.Title,
        //        Composer = t.Composer,
        //        Lyricist = t.Lyricist
        //    }).ToList();
        //}

        //public async Task<ICollection<TrackInfoDto>> GetTracksWithPaginationAsync(int pageNumber, int pageSize)
        //{
        //    var tracks = await _trackrepository.GetPagedAsync(pageNumber, pageSize);
        //    return tracks.Select(t => new TrackInfoDto(t)
        //    {
        //        Title = t.Title,
        //        Subtitle = t.Subtitle,
        //        Description = t.Description,
        //        Albumtitle = t.Album?.Title,
        //        Composer = t.Composer,
        //        Lyricist = t.Lyricist
        //    }).ToList();
        //}
        #endregion
    }
}
