using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using CloudApp.Core.Interface;

namespace CloudApp.Service.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IRepository<Album> _albumRepository;

        public AlbumService(IRepository<Album> albumRepository)
        {
            _albumRepository = albumRepository;
        }

        #region 查询方法
        /// <summary>
        /// 获取所有专辑
        /// </summary>
        /// <returns></returns>
        public ICollection<AlbumInfoDto> GetAllAlbums()
        {
            var albums = this._albumRepository.GetAll();

            return albums.Select(a => new AlbumInfoDto(a)).ToList();
        }

        /// <summary>
        /// 根据id获取专辑信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public AlbumInfoDto GetAlbumById(int id)
        {
            var album = this._albumRepository.GetById(id);
            if (album == null)
            {
                throw new ArgumentException(nameof(id), "专辑不存在");
            }
            return new AlbumInfoDto(album);
        }

        #endregion

        #region 操作方法
        public int AddAlbum(CreateAlbumDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            Album album = model.ToAlbum();
            this._albumRepository.Add(album);
            return album.Id;
        }
        
        public void DeleteAlbum(int id)
        {
            // 检查专辑是否存在
            bool albumExists = this._albumRepository.Exists(id);
            if (!albumExists)
            {
                throw new ArgumentException(nameof(id), "专辑不存在");
            }
            
            this._albumRepository.Delete(id);
        }
        
        public void UpdateAlbum(int id, CreateAlbumDto model)
        {
            Album album = _albumRepository.GetById(id);
            if (album != null)
            {
                album = model.ToAlbum();
            }
        }
        #endregion

        // 异步方法（新增）
        //public async Task AddAlbumAsync(CreateAlbumDto model)
        //{
        //    if (model == null)
        //    {
        //        throw new ArgumentNullException(nameof(model));
        //    }
        //    Album album = new Album
        //    {
        //        Title = model.Title,
        //        Description = model.Description,
        //        Artist = model.Artist,
        //        ReleaseDate = DateTime.UtcNow,
        //        CoverImageUrl = model.CoverImageUrl
        //    };

        //    await this._albumRepository.AddAsync(album);
        //}

        //public async Task DeleteAlbumAsync(int id)
        //{
        //    // 检查专辑是否存在
        //    bool albumExists = await this._albumRepository.ExistsAsync(id);
        //    if (!albumExists)
        //    {
        //        throw new ArgumentException(nameof(id), "专辑不存在");
        //    }

        //    await this._albumRepository.DeleteAsync(id);
        //}

        //public async Task<AlbumInfoDto> GetAlbumByIdAsync(int id)
        //{
        //    var album = await this._albumRepository.GetByIdAsync(id);
        //    if (album == null)
        //    {
        //        throw new ArgumentException(nameof(id), "专辑不存在");
        //    }

        //    return new AlbumInfoDto(album);
        //}

        //public async Task<ICollection<AlbumInfoDto>> GetAllAlbumsAsync()
        //{
        //    var albums = await this._albumRepository.GetAllAsync();

        //    return albums.Select(a => new AlbumInfoDto(a)).ToList();
        //}

        //public async Task UpdateAlbumAsync(int id, CreateAlbumDto model)
        //{
        //    Album album = await _albumRepository.GetByIdAsync(id);
        //    if (album != null)
        //    {
        //        album.Title = model.Title;
        //        album.Description = model.Description;
        //        album.Artist = model.Artist;
        //        album.CoverImageUrl = model.CoverImageUrl;
        //        await _albumRepository.UpdateAsync(album);
        //    }
        //}

        // 扩展功能（新增）
        //public ICollection<AlbumInfoDto> GetAlbumsByTitle(string title)
        //{
        //    if (_albumRepository is AlbumRepository albumRepo)
        //    {
        //        return albumRepo.GetAlbumsByTitle(title)
        //            .Select(a => new AlbumInfoDto(a))
        //            .ToList();
        //    }
        //    // 降级处理
        //    var albums = _albumRepository.GetByCondition(a => a.Title.Contains(title));
        //    return albums.Select(a => new AlbumInfoDto(a)).ToList();
        //}

        //public async Task<ICollection<AlbumInfoDto>> GetAlbumsByTitleAsync(string title)
        //{
        //    if (_albumRepository is AlbumRepository albumRepo)
        //    {
        //        var result = await albumRepo.GetAlbumsByTitleAsync(title);
        //        return result.Select(a => new AlbumInfoDto(a)).ToList();
        //    }
        //    // 降级处理
        //    var albums = await _albumRepository.GetByConditionAsync(a => a.Title.Contains(title));
        //    return albums.Select(a => new AlbumInfoDto(a)).ToList();
        //}

        //public ICollection<AlbumInfoDto> GetAlbumsWithPagination(int pageNumber, int pageSize)
        //{
        //    var albums = _albumRepository.GetPaged(pageNumber, pageSize);
        //    return albums.Select(a => new AlbumInfoDto(a)).ToList();
        //}

        //public async Task<ICollection<AlbumInfoDto>> GetAlbumsWithPaginationAsync(int pageNumber, int pageSize)
        //{
        //    var albums = await _albumRepository.GetPagedAsync(pageNumber, pageSize);
        //    return albums.Select(a => new AlbumInfoDto(a)).ToList();
        //}
    }
}
