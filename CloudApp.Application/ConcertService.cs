using CloudApp.Core.Dtos.Concert;
using CloudApp.Core.Entities;
using CloudApp.Core.Exceptions;
using CloudApp.Core.Extensions;
using CloudApp.Core.Interfaces.Repositories;
using CloudApp.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace CloudApp.Application
{
    public class ConcertService : IConcertService
    {
        private readonly IConcertRepository _concertRepository;
        private readonly ILogger<ConcertService> _logger;

        public ConcertService(IConcertRepository repository, ILogger<ConcertService> logger)
        {
            _concertRepository = repository;
            _logger = logger;
        }

        #region 同步方法
        public void CreateConcert(CreateConcertRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            _logger.LogInformation("开始添加演唱会: {Title}, 地址: {Address}", request.Title, request.Address);

            if (_concertRepository.ConcertExists(request.Title))
                throw new BusinessException($"演唱会《{request.Title}》已存在");

            Concert concert = request.ToEntity();
            _concertRepository.Add(concert);
            _concertRepository.SaveChange();

            _logger.LogInformation("成功添加演唱会: ID={ConcertId}, Title={Title}", concert.Id, concert.Title);
        }

        public void DeleteConcert(int id)
        {
            if (id <= 0)
                throw new ArgumentException("演唱会ID无效", nameof(id));

            _logger.LogInformation("开始删除演唱会: ID={ConcertId}", id);

            var concert = _concertRepository.GetById(id);
            if (concert == null)
                throw new EntityNotFoundException("演唱会", id);

            concert.Delete();
            _concertRepository.SaveChange();

            _logger.LogInformation("成功删除演唱会: ID={ConcertId}", id);
        }

        public void UpdateConcert(int id, CreateConcertRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            if (id <= 0)
                throw new BusinessException("演唱会ID无效");

            _logger.LogInformation("开始更新演唱会: ID={ConcertId}", id);
            var concert = _concertRepository.GetById(id);
            if (concert == null)
                throw new EntityNotFoundException("演唱会", id);

            var existing = _concertRepository.FindByTitle(request.Title);
            if (existing != null && existing.Id != id)
                throw new BusinessException($"演唱会 '{request.Title}' 已存在");

            concert.Title = request.Title;
            concert.Description = request.Description;
            concert.UpdatedAt = DateTime.UtcNow;

            _concertRepository.Update(concert);
            _concertRepository.SaveChange();

            _logger.LogInformation("成功更新演唱会: ID={ConcertId}, Title={Title}", concert.Id, concert.Title);
        }

        public ConcertInfoDto? GetById(int id)
        {
            try
            {
                _logger.LogDebug("开始获取演唱会详情: ID={ConcertId}", id);
                Concert? concert = _concertRepository.GetById(id);
                if (concert == null)
                {
                    _logger.LogWarning("未找到演唱会: ID={ConcertId}", id);
                    //throw new EntityNotFoundException("演唱会", id);
                    return null;
                }
                _logger.LogInformation("成功获取演唱会详情: ID={ConcertId}, Title={Title}", concert.Id, concert.Title);
                return concert.ToInfoDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取演唱会详情失败: ID={ConcertId}", id);
                throw;
            }
        }
        #endregion
    }
}