using CloudApp.Core.Dtos.Concert;
using CloudApp.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [Authorize(Roles = "Admin")]
        public ActionResult CreateConcert([FromBody] CreateConcertRequest model)
        {
            if (model != null)
                _concertService.CreateConcert(model);

            return Ok($"成功新增演唱会,{model.Title}");
        }

        [HttpDelete("{concertId}")]
        [Authorize(Roles = "Admin")]
        public ActionResult DelectConcert(int concertId)
        {
            _logger.LogInformation("收到删除演唱会请求: ID={ConcertId}", concertId);

            _concertService.DeleteConcert(concertId);
            return Ok("成功删除演唱会");
        }

        [HttpPatch]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateConcert(int concertId, [FromBody] CreateConcertRequest model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("更新演唱会请求验证失败: ID={ConcertId}, {Errors}", concertId,
                    string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
                return BadRequest("存在非法数据，更新失败");
            }

            _concertService.UpdateConcert(concertId, model);
            return Ok("成功更新演唱会");
        }

        [HttpGet]
        public ActionResult<List<ConcertInfoDto>> GetAll()
        {
            var res = _concertService.GetAllConcerts();
            return Ok(res);
        }

        [HttpGet]
        public ActionResult<ConcertInfoDto> GetById(int id)
        {
            _logger.LogDebug("收到获取演唱会详情请求: ID={ConcertId}", id);

            var infoDto = _concertService.GetById(id);
            if (infoDto == null)
            {
                return BadRequest("未找到对应演唱会");
            }
            return Ok(infoDto);
        }
    }
}
