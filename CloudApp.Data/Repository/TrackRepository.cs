using CloudApp.Core.Entities;
using CloudApp.Core.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CloudApp.Data.Repository
{
    public class TrackRepository : BaseRepository<Track>, IRepository<Track>
    {
        public TrackRepository(MyDBContext dbContext) : base(dbContext)
        {
        }

        #region 重写基础仓储方法
        public override IEnumerable<Track> GetAll()
        {
            return _dbSet.Include(t => t.Album).ToList();
        }

        public override Track GetById(int id)
        {
            return _dbSet.Include(t => t.Album).FirstOrDefault(t => t.Id == id);
        }

        //public override async Task<IEnumerable<Track>> GetAllAsync()
        //{
        //    return await _dbSet.Include(t => t.Album).ToListAsync();
        //}

        //public override async Task<Track> GetByIdAsync(int id)
        //{
        //    return await _dbSet.Include(t => t.Album).FirstOrDefaultAsync(t => t.Id == id);
        //}
        #endregion

        #region Track特有查询方法
        // 根据标题查询曲目
        public IEnumerable<Track> GetTracksByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                return Enumerable.Empty<Track>();
            
            return _dbSet
                .Include(t => t.Album)
                .Where(t => t.Title.Contains(title))
                .ToList();
        }

        // 异步根据标题查询曲目
        public async Task<IEnumerable<Track>> GetTracksByTitleAsync(string title)
        {
            if (string.IsNullOrEmpty(title))
                return Enumerable.Empty<Track>();
            
            return await _dbSet
                .Include(t => t.Album)
                .Where(t => t.Title.Contains(title))
                .ToListAsync();
        }

        // 根据专辑ID查询曲目
        public ICollection<Track> GetTracksByAlbumId(int albumId)
        {
            return _dbSet
                .Include(t => t.Album)
                .Where(t => t.AlbumId == albumId)
                .ToList();
        }

        // 异步根据专辑ID查询曲目
        public async Task<IEnumerable<Track>> GetTracksByAlbumIdAsync(int albumId)
        {
            return await _dbSet
                .Include(t => t.Album)
                .Where(t => t.AlbumId == albumId)
                .ToListAsync();
        }

        // 根据专辑标题查询曲目
        public IEnumerable<Track> GetTracksByAlbumTitle(string albumTitle)
        {
            if (string.IsNullOrEmpty(albumTitle))
                return Enumerable.Empty<Track>();
            
            return _dbSet
                .Include(t => t.Album)
                .Where(t => t.Album != null && t.Album.Title.Contains(albumTitle))
                .ToList();
        }

        // 异步根据专辑标题查询曲目
        public async Task<IEnumerable<Track>> GetTracksByAlbumTitleAsync(string albumTitle)
        {
            if (string.IsNullOrEmpty(albumTitle))
                return Enumerable.Empty<Track>();
            
            return await _dbSet
                .Include(t => t.Album)
                .Where(t => t.Album != null && t.Album.Title.Contains(albumTitle))
                .ToListAsync();
        }

        // 获取最新添加的曲目
        public IEnumerable<Track> GetLatestTracks(int count)
        {
            return _dbSet
                .Include(t => t.Album)
                .OrderByDescending(t => t.Id) // 假设Id递增表示最新添加
                .Take(count)
                .ToList();
        }

        // 异步获取最新添加的曲目
        public async Task<IEnumerable<Track>> GetLatestTracksAsync(int count)
        {
            return await _dbSet
                .Include(t => t.Album)
                .OrderByDescending(t => t.Id) // 假设Id递增表示最新添加
                .Take(count)
                .ToListAsync();
        }

        //// 分页获取曲目，包含专辑
        //public override IEnumerable<Track> GetPaged(int pageNumber, int pageSize)
        //{
        //    return _dbSet
        //        .Include(t => t.Album)
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToList();
        //}

        //// 异步分页获取曲目，包含专辑
        //public override async Task<IEnumerable<Track>> GetPagedAsync(int pageNumber, int pageSize)
        //{
        //    return await _dbSet
        //        .Include(t => t.Album)
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToListAsync();
        //}
        #endregion
    }
}
