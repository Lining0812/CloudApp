using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using CloudApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CloudApp.WebApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly ITrackService _trackService;
        public TrackController(ITrackService trackService)
        {
            _trackService = trackService;
        }

        [HttpPost]
        public ActionResult AddTrack([FromForm] CreateTrackDto model)
        {
            if (ModelState.IsValid)
            {
                _trackService.AddTrack(model);
                return Ok("成功新增单曲");
            }
            return BadRequest("存在非法数据，添加失败");
        }

        [HttpGet]
        public ActionResult<ICollection<Track>> GetAllTracks()
        {
            var tracks = _trackService.GetAllTracks();
            return Ok(tracks);
        }
    }
}
