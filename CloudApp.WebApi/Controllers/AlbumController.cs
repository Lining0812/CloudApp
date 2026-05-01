using CloudApp.Core.Dtos.Album;
using CloudApp.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult CreateAlbum([FromForm] CreateAlbumRequest model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("添加专辑请求验证失败: {Errors}",
                    string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
                return BadRequest("存在非法数据，添加失败");
            }
            if (model != null) _albumService.CreateAlbum(model);
            return Ok("成功新增专辑");
        }

        [HttpDelete("{albumId}")]
        public ActionResult DeleteAlbum(int albumId)
        {
            _albumService.DeleteAlbum(albumId);
            return Ok("成功删除专辑");
        }

        [HttpPatch("{albumId}")]
        public ActionResult UpdateAlbum(int albumId, [FromForm] CreateAlbumRequest model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("更新专辑请求验证失败: ID={AlbumId}, {Errors}", albumId,
                    string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
                return BadRequest("存在非法数据，更新失败");
            }

            if (model != null) _albumService.UpdateAlbum(albumId, model);
            return Ok("成功更新专辑");
        }

        [HttpGet]
        public ActionResult<ICollection<AlbumInfoDto>> GetAllAlbums()
        {
            var albums = _albumService.GetAllAlbums();
            return Ok(albums);
        }
    }
}
