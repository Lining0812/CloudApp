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
        public ConcertService(IConcertRepository repository)
        {
            _concertRepository = repository;
        }

        #region 同步方法
        public void AddConcert(CreateConcertDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            string url = "";
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

        public void UpdateConcert(int id, CreateConcertDto model)
        {
            Concert concert = _concertRepository.GetById(id);
            if (concert == null)
            {
                throw new ArgumentException($"演唱会不存在 (ID: {id})");
            }
            else
            {
                concert.Title = model.Title;
                concert.Description = model.Description;
                concert.UpdatedAt = DateTime.UtcNow;

                _concertRepository.Update(concert);
                _concertRepository.SaveChange();
            }
        }

        public ConcertInfoDto? GetById(int id)
        {
            Concert concert = _concertRepository.GetById(id);
            if (concert == null)
            {
                throw new ArgumentException("演唱会不存在");
            }
            return concert.ToInfoDto();
        }
        #endregion
    }
}