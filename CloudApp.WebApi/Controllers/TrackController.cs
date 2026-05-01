using CloudApp.Core.Dtos.Track;
using CloudApp.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudApp.WebApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly ITrackService _trackService;
        private readonly ILogger<TrackController> _logger;

        public TrackController(ITrackService trackService, ILogger<TrackController> logger)
        {
            _trackService = trackService;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult CreateTrack([FromForm] CreateTrackDto model)
        {
            _logger.LogInformation($"收到添加单曲请求: Title={model?.Title}, Artist={model?.Artist}");

            if (model == null) return BadRequest("请求数据不能为空");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("添加单曲请求验证失败: {Errors}",
                    string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
                return BadRequest("存在非法数据，添加失败");
            }

            _trackService.CreateTrack(model);
            return Ok("成功新增单曲");
        }

        [HttpDelete("{trackId}")]
        public ActionResult DeleteTrack(int trackId)
        {
            _logger.LogInformation("收到删除单曲请求: ID={TrackId}", trackId);

            _trackService.DeleteTrack(trackId);
            return Ok("成功删除单曲");
        }

        [HttpPatch("{trackId}")]
        public ActionResult UpdateTrack(int trackId, [FromForm] CreateTrackDto model)
        {
            _logger.LogInformation("收到更新单曲请求: ID={TrackId}, Title={Title}", trackId, model?.Title);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("更新单曲请求验证失败: ID={TrackId}, {Errors}", trackId,
                    string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
                return BadRequest("存在非法数据，更新失败");
            }

            if (model != null) _trackService.UpdateTrack(trackId, model);
            return Ok("成功更新单曲");
        }

        [HttpGet]
        public ActionResult<ICollection<TrackInfoDto>> GetAll()
        {
            _logger.LogDebug("收到获取所有单曲请求");

            var tracks = _trackService.GetAllTracks();
            return Ok(tracks);
        }

        [HttpGet]
        public ActionResult<TrackInfoDto> GetById(int id)
        {
            _logger.LogDebug("收到获取单曲详情请求: ID={TrackId}", id);

            try
            {
                var infoDto = _trackService.GetById(id);
                if (infoDto == null)
                {
                    _logger.LogWarning("未找到单曲: ID={TrackId}", id);
                    return BadRequest("未找到对应单曲");
                }
                _logger.LogInformation("成功处理获取单曲详情请求: ID={TrackId}", id);
                return Ok(infoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理获取单曲详情请求时发生错误: ID={TrackId}", id);
                throw;
            }
        }
    }
}
