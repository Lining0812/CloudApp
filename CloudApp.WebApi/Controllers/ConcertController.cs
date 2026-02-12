using CloudApp.Core.Dtos.Concert;
using CloudApp.Core.Entities;
using CloudApp.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudApp.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConcertController : ControllerBase
    {
        private readonly IConcertService _concertService;
        private readonly ILogger<ConcertController> _logger;

        public ConcertController(IConcertService service, ILogger<ConcertController> logger)
        {
            _concertService = service;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult AddConcert([FromForm]CreateConcertDto model)
        {
            _logger.LogInformation("收到添加演唱会请求: Title={Title}, Address={Address}", model?.Title, model?.Address);
            
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("添加演唱会请求验证失败: {Errors}",
                    string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
                return BadRequest("存在非法数据，添加失败");
            }
            
            try
            {
                _concertService.AddConcert(model);
                _logger.LogInformation("成功处理添加演唱会请求: Title={Title}", model.Title);
                return Ok("成功新增演唱会");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理添加演唱会请求时发生错误: Title={Title}", model?.Title);
                throw;
            }
        }

        [HttpDelete("{concertId}")]
        public ActionResult DelectConcert(int concertId)
        {
            _logger.LogInformation("收到删除演唱会请求: ID={ConcertId}", concertId);
            
            try
            {
                _concertService.DelectConcert(concertId);
                _logger.LogInformation("成功处理删除演唱会请求: ID={ConcertId}", concertId);
                return Ok("成功删除演唱会");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理删除演唱会请求时发生错误: ID={ConcertId}", concertId);
                throw;
            }
        }

        [HttpPatch]
        public ActionResult UpdateConcert(int concertId, [FromForm] CreateConcertDto model)
        {
            _logger.LogInformation("收到更新演唱会请求: ID={ConcertId}, Title={Title}", concertId, model?.Title);
            
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("更新演唱会请求验证失败: ID={ConcertId}, {Errors}", concertId,
                    string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
                return BadRequest("存在非法数据，更新失败");
            }
            
            try
            {
                _concertService.UpdateConcert(concertId, model);
                _logger.LogInformation("成功处理更新演唱会请求: ID={ConcertId}", concertId);
                return Ok("成功更新演唱会");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理更新演唱会请求时发生错误: ID={ConcertId}", concertId);
                throw;
            }
        }

        [HttpGet]
        public ActionResult<ICollection<ConcertInfoDto>> GetAll()
        {
            _logger.LogWarning("GetAll方法尚未实现，返回测试消息");
            return Ok("成功获取演唱会，仅定义测试");
        }

        [HttpGet]
        public ActionResult<ConcertInfoDto> GetById(int id)
        {
            _logger.LogDebug("收到获取演唱会详情请求: ID={ConcertId}", id);
            
            try
            {
                var infoDto = _concertService.GetById(id);
                if(infoDto == null)
                {
                    _logger.LogWarning("未找到演唱会: ID={ConcertId}", id);
                    return BadRequest("未找到对应演出");
                }
                _logger.LogInformation("成功处理获取演唱会详情请求: ID={ConcertId}", id);
                return Ok(infoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理获取演唱会详情请求时发生错误: ID={ConcertId}", id);
                throw;
            }
        }
    }
}
