using CloudApp.Core.Dtos;
using CloudApp.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudApp.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConcertController : ControllerBase
    {
        private IConcertService _concertService;
        public ConcertController(IConcertService concertService)
        {
            _concertService = concertService;
        }

        [HttpPost]
        public ActionResult AddConcert([FromForm]CreateConcertDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest($"存在非法数据，添加失败");
            }
            _concertService.AddConcert(dto);
            return Ok("成功新增演唱会");
        }
    }
}
