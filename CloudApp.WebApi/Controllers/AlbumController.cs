using CloudApp.Core.Dtos;
using CloudApp.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudApp.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpPost]
        public ActionResult AddAlbum([FromForm] CreateAlbumDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("存在非法数据，添加失败");
            }
                _albumService.AddAlbum(model);
            return Ok("成功新增专辑");
        }

        [HttpDelete("{albumId}")]
        public ActionResult DeleteAlbum(int albumId)
        {
            _albumService.DeleteAlbum(albumId);
            return Ok("成功删除专辑");
        }

        [HttpPatch("{albumId}")]
        public ActionResult UpdateAlbum(int albumId, [FromForm] CreateAlbumDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("存在非法数据，更新失败");
            }
            _albumService.UpdateAlbum(albumId, model);
            return Ok("成功更新专辑");
        }

        [HttpGet]
        public ActionResult<ICollection<AlbumInfoDto>> GetAllAlbums()
        {
            var albums = _albumService.GetAllAlbums();
            return Ok(albums);
        }

        [HttpGet("{albumId}")]
        public ActionResult<AlbumInfoDto> GetAlbumById(int albumId)
        {
            var album = _albumService.GetAlbumById(albumId);
            if(album == null)
            {
                return NotFound("未找到对应专辑");
            }
            return Ok(album);
        }
    }
}
