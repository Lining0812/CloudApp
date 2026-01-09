using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using CloudApp.Core.Extensions;
using CloudApp.Core.Interfaces.Repositories;
using CloudApp.Core.Interfaces.Services;
using CloudApp.Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace CloudApp.Service
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
        public void AddConcert(CreateConcertDto model)
        {
            if (model == null)
            {
                _logger.LogWarning("尝试添加演唱会时，模型为null");
                throw new ArgumentNullException(nameof(model));
            }

            try
            {
                _logger.LogInformation("开始添加演唱会: {Title}, 地址: {Address}", model.Title, model.Address);
                string url = "";
                Concert concert = model.ToEntity(url);
                _concertRepository.Add(concert);
                _concertRepository.SaveChange();
                _logger.LogInformation("成功添加演唱会: ID={ConcertId}, Title={Title}", concert.Id, concert.Title);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "添加演唱会失败: Title={Title}, Address={Address}", model.Title, model.Address);
                throw;
            }
        }

        public void DelectConcert(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("尝试删除演唱会时，ID无效: {ConcertId}", id);
                throw new ArgumentException("演唱会ID无效", nameof(id));
            }

            try
            {
                _logger.LogInformation("开始删除演唱会: ID={ConcertId}", id);
                bool concertExists = _concertRepository.Exists(id);
                if (!concertExists)
                {
                    _logger.LogWarning("尝试删除不存在的演唱会: ID={ConcertId}", id);
                    throw new EntityNotFoundException("演唱会", id);
                }
                _concertRepository.Delete(id);
                _concertRepository.SaveChange();
                _logger.LogInformation("成功删除演唱会: ID={ConcertId}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除演唱会失败: ID={ConcertId}", id);
                throw;
            }
        }

        public void UpdateConcert(int id, CreateConcertDto model)
        {
            if (model == null)
            {
                _logger.LogWarning("尝试更新演唱会时，模型为null: ID={ConcertId}", id);
                throw new ArgumentNullException(nameof(model));
            }

            try
            {
                _logger.LogInformation("开始更新演唱会: ID={ConcertId}", id);
                Concert? concert = _concertRepository.GetById(id);
                if (concert == null)
                {
                    _logger.LogWarning("尝试更新不存在的演唱会: ID={ConcertId}", id);
                    throw new EntityNotFoundException("演唱会", id);
                }

                concert.Title = model.Title;
                concert.Description = model.Description;
                concert.UpdatedAt = DateTime.UtcNow;

                _concertRepository.Update(concert);
                _concertRepository.SaveChange();
                _logger.LogInformation("成功更新演唱会: ID={ConcertId}, Title={Title}", concert.Id, concert.Title);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新演唱会失败: ID={ConcertId}", id);
                throw;
            }
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
                    throw new EntityNotFoundException("演唱会", id);
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