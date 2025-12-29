using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using CloudApp.Core.Extensions;
using CloudApp.Core.Interfaces.Repositories;
using CloudApp.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;

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

        public ConcertInfoDto? GetById(int id)
        {
            var concert = _concertRepository.GetById(id);

            if (concert == null)
            {
                throw new ArgumentException("演唱会不存在");
            }

            if (!string.IsNullOrEmpty(concert.CoverImageUrl))
            {
                var (stream, contentType) = GetCoverImage(concert);
                stream.Position = 0;

                string fileName = Path.GetFileName(concert.CoverImageUrl);
                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = $"concert_{id}_cover.jpg";
                }
                IFormFile image = new FormFile(
                    baseStream: stream,
                    baseStreamOffset: 0,
                    length: stream.Length,
                    name: "CoverImage",
                    fileName: fileName
                    );
                return concert.ToInfoDto();
            }
            return null;
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
