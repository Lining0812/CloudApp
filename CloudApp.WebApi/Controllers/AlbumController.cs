using CloudApp.Core.Dtos;
using CloudApp.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudApp.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        private readonly ILogger<AlbumController> _logger;

        public AlbumController(IAlbumService albumService, ILogger<AlbumController> logger)
        {
            _albumService = albumService;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult AddAlbum([FromForm] CreateAlbumDto model)
        {
            _logger.LogInformation("收到添加专辑请求: Title={Title}, Artist={Artist}", model?.Title, model?.Artist);
            
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("添加专辑请求验证失败: {Errors}", 
                    string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
                return BadRequest("存在非法数据，添加失败");
            }
            
            try
            {
                _albumService.AddAlbum(model);
                _logger.LogInformation("成功处理添加专辑请求: Title={Title}", model.Title);
                return Ok("成功新增专辑");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理添加专辑请求时发生错误: Title={Title}", model?.Title);
                throw; // 异常会被全局异常处理中间件捕获
            }
        }

        [HttpDelete("{albumId}")]
        public ActionResult DeleteAlbum(int albumId)
        {
            _logger.LogInformation("收到删除专辑请求: ID={AlbumId}", albumId);
            
            try
            {
                _albumService.DeleteAlbum(albumId);
                _logger.LogInformation("成功处理删除专辑请求: ID={AlbumId}", albumId);
                return Ok("成功删除专辑");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理删除专辑请求时发生错误: ID={AlbumId}", albumId);
                throw;
            }
        }

        [HttpPatch("{albumId}")]
        public ActionResult UpdateAlbum(int albumId, [FromForm] CreateAlbumDto model)
        {
            _logger.LogInformation("收到更新专辑请求: ID={AlbumId}, Title={Title}", albumId, model?.Title);
            
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("更新专辑请求验证失败: ID={AlbumId}, {Errors}", albumId,
                    string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
                return BadRequest("存在非法数据，更新失败");
            }
            
            try
            {
                _albumService.UpdateAlbum(albumId, model);
                _logger.LogInformation("成功处理更新专辑请求: ID={AlbumId}", albumId);
                return Ok("成功更新专辑");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理更新专辑请求时发生错误: ID={AlbumId}", albumId);
                throw;
            }
        }

        [HttpGet]
        public ActionResult<ICollection<AlbumInfoDto>> GetAll()
        {
            _logger.LogDebug("收到获取所有专辑请求");
            
            try
            {
                var albums = _albumService.GetAllAlbums();
                _logger.LogInformation("成功处理获取所有专辑请求，返回 {Count} 条记录", albums.Count);
                return Ok(albums);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理获取所有专辑请求时发生错误");
                throw;
            }
        }

        [HttpGet("{albumId}")]
        public ActionResult<AlbumInfoDto> GetById(int albumId)
        {
            _logger.LogDebug("收到获取专辑详情请求: ID={AlbumId}", albumId);
            
            try
            {
                var album = _albumService.GetAlbumById(albumId);
                if(album == null)
                {
                    _logger.LogWarning("未找到专辑: ID={AlbumId}", albumId);
                    return NotFound("未找到对应专辑");
                }
                _logger.LogInformation("成功处理获取专辑详情请求: ID={AlbumId}", albumId);
                return Ok(album);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理获取专辑详情请求时发生错误: ID={AlbumId}", albumId);
                throw;
            }
        }
    }
}
