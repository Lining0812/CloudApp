using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using CloudApp.Core.Interfaces.Services;
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
                return Ok("添加单曲成功");
            }
            return BadRequest("存在非法数据，更新失败");
        }

        [HttpDelete("{trackId}")]
        public ActionResult DeleteTrack(int trackId)
        {
            _trackService.DeleteTrack(trackId);
            return Ok("删除单曲成功");
        }

        [HttpPatch("{trackId}")]
        public ActionResult UpdateTrack(int trackId, [FromForm] CreateTrackDto model)
        {
            if (ModelState.IsValid)
            {
                _trackService.UpdateTrack(trackId, model);
                return Ok("更新单曲成功");
            }
            return BadRequest("存在非法数据，更新失败");
        }

        [HttpGet]
        public ActionResult<ICollection<Track>> GetAllTracks()
        {
            var tracks = _trackService.GetAllTracks();
            return Ok(tracks);
        }
    }
}
