using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using CloudApp.Core.Interfaces.Services;
using CloudApp.Service;
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
            if (!ModelState.IsValid)
            {
                return BadRequest("存在非法数据，添加失败");
            }
            _trackService.AddTrack(model);
            return Ok("成功新增单曲");
        }

        [HttpDelete("{trackId}")]
        public ActionResult DeleteTrack(int trackId)
        {
            _trackService.DeleteTrack(trackId);
            return Ok("成功删除单曲");
        }

        [HttpPatch("{trackId}")]
        public ActionResult UpdateTrack(int trackId, [FromForm] CreateTrackDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("存在非法数据，更新失败");
            }
            _trackService.UpdateTrack(trackId, model);
            return Ok("成功更新单曲");
        }

        [HttpGet]
        public ActionResult<ICollection<TrackInfoDto>> GetAll()
        {
            var tracks = _trackService.GetAllTracks();
            return Ok(tracks);
        }

        [HttpGet]
        public ActionResult<TrackInfoDto> GetById(int id)
        {
            var infoDto = _trackService.GetById(id);
            if (infoDto == null)
            {
                return BadRequest("未找到对应演出");
            }
            return Ok(infoDto);
        }
    }
}
