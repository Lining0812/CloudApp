using CloudApp.Core.Dtos.Track;
using CloudApp.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// 新增单曲（返回创建后的单曲信息）
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TrackInfoDto>> CreateTrack([FromBody] TrackCreateDto model, CancellationToken ct = default)
        {
            _logger.LogInformation("收到添加单曲请求: Title={Title}, Artist={Artist}", model?.Title, model?.Artist);
            if (model == null) return BadRequest("单曲数据不能为空");

            var dto = await _trackService.CreateTrackAsync(model, ct);
            return Ok(dto);
        }

        /// <summary>
        /// 更新单曲（局部更新，只传需要改的字段）
        /// </summary>
        [HttpPatch("{trackId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TrackInfoDto>> UpdateTrack(int trackId, [FromBody] TrackUpdateDto model, CancellationToken ct = default)
        {
            _logger.LogInformation("收到更新单曲请求: ID={TrackId}, Title={Title}", trackId, model?.Title);
            if (model == null) return BadRequest("单曲数据不能为空");

            var dto = await _trackService.UpdateTrackAsync(trackId, model, ct);
            return Ok(dto);
        }

        /// <summary>
        /// 删除单曲（软删除）
        /// </summary>
        [HttpDelete("{trackId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteTrack(int trackId, CancellationToken ct = default)
        {
            _logger.LogInformation("收到删除单曲请求: ID={TrackId}", trackId);
            await _trackService.DeleteTrackAsync(trackId, ct);
            return Ok("成功删除单曲");
        }

        /// <summary>
        /// 获取所有单曲
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ICollection<TrackInfoDto>>> GetAll(CancellationToken ct = default)
        {
            _logger.LogDebug("收到获取所有单曲请求");
            var tracks = await _trackService.GetAllTracksAsync(ct);
            return Ok(tracks);
        }

        /// <summary>
        /// 根据Id获取单曲详情（不存在返回 404）
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<TrackInfoDto>> GetById(int id, CancellationToken ct = default)
        {
            _logger.LogDebug("收到获取单曲详情请求: ID={TrackId}", id);
            var infoDto = await _trackService.GetByIdAsync(id, ct);
            return Ok(infoDto);
        }

        /// <summary>
        /// 获取专辑下的曲目
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ICollection<TrackInfoDto>>> GetByAlbumId(int albumId, CancellationToken ct = default)
        {
            _logger.LogDebug("收到获取专辑曲目请求: AlbumId={AlbumId}", albumId);
            var tracks = await _trackService.GetTracksByAlbumIdAsync(albumId, ct);
            return Ok(tracks);
        }

        /// <summary>
        /// 按标题模糊搜索单曲
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ICollection<TrackInfoDto>>> Search(string keyword, CancellationToken ct = default)
        {
            _logger.LogDebug("收到搜索单曲请求: Keyword={Keyword}", keyword);
            var tracks = await _trackService.SearchTracksAsync(keyword, ct);
            return Ok(tracks);
        }
    }
}
