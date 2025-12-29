using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using CloudApp.Core.Extensions;
using CloudApp.Core.Interfaces.Repositories;
using CloudApp.Core.Interfaces.Services;

namespace CloudApp.Service
{
    public class ConcertService : IConcertService
    {
        private readonly IConcertRepository _concertRepository;
        private readonly IImageStorageService _imageStorageService;
        public ConcertService(IConcertRepository repository, IImageStorageService imageStorageService)
        {
            _concertRepository = repository;
            _imageStorageService = imageStorageService;
        }

        #region 同步方法
        public void AddConcert(CreateConcertDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            string url = _imageStorageService.SaveConcertImage(model.CoverImage);
            Concert concert = model.ToEntity(url);
            _concertRepository.Add(concert);
            _concertRepository.SaveChange();
        }

        public void DelectConcert(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException($"专辑id无效 (ID: {id})");
            }
            bool albumExists = _concertRepository.Exists(id);
            if (!albumExists)
            {
                throw new ArgumentException($"专辑不存在 (ID: {id})");
            }
            _concertRepository.Delete(id);
            _concertRepository.SaveChange();
        }

        public ConcertInfoDto? GetById(int id)
        {
            var concert = _concertRepository.GetById(id);

            if (concert == null)
            {
                throw new ArgumentException("演唱会不存在");
            }
            return concert.ToInfoDto();
        }
        
        public (Stream stream, string contentType) GetCoverImage(Concert concert)
        {
            // 使用ImageStorageService获取图片
            var (stream, contentType) = _imageStorageService.GetImage(concert.CoverImageUrl);
            return (stream, contentType);
        }
        #endregion
    }
}