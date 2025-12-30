using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using CloudApp.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudApp.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConcertController : ControllerBase
    {
        private IConcertService _concertService;
        public ConcertController(IConcertService Service)
        {
            _concertService = Service;
        }

        [HttpPost]
        public ActionResult AddConcert([FromForm]CreateConcertDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest($"存在非法数据，添加失败");
            }
            _concertService.AddConcert(model);
            return Ok("成功新增演唱会");
        }

        [HttpDelete("{concertId}")]
        public ActionResult DelectConcert(int concertId)
        {
            _concertService.DelectConcert(concertId);
            return Ok("成功删除演唱会");
        }

        [HttpPatch]
        public ActionResult UpdateConcert(int concertId, [FromForm] CreateConcertDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("存在非法数据，更新失败");
            }
            _concertService.UpdateConcert(concertId, model);
            return Ok("成功更新演唱会");
        }

        [HttpGet]
        public ActionResult<ICollection<ConcertInfoDto>> GetAll()
        {
            return Ok("成功获取演唱会，仅定义测试");
        }

        [HttpGet]
        public ActionResult<ConcertInfoDto> GetById(int id)
        {
            var infoDto = _concertService.GetById(id);
            if(infoDto == null)
            {
                return BadRequest("未找到对应演出");
            }
            return Ok(infoDto);
        }
    }
}
