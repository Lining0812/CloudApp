using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using CloudApp.Core.Extensions;
using CloudApp.Core.Interfaces.Repositories;
using CloudApp.Core.Interfaces.Services;

namespace CloudApp.Service
{
    public class ConcertService : IConcertService
    {
        private readonly IRepository<Concert> _concertRepository;
        public ConcertService(IRepository<Concert> repository)
        {
            _concertRepository = repository;
        }

        #region 同步方法
        public void AddConcert(CreateConcertDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            Concert concert = dto.ToEntity();
            _concertRepository.Add(concert);
            _concertRepository.SaveChange();
        }
        #endregion
    }
}
