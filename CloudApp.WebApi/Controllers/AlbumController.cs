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
        public ActionResult AddAlbum([FromForm] CreateAlbumDto albumDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest($"存在非法数据，添加失败");
            }
            _albumService.AddAlbum(albumDto);
            return Ok("成功新增专辑");
        }

        [HttpDelete]
        public ActionResult DeleteAlbum(int id)
        {
            _albumService.DeleteAlbum(id);
            return Ok("成功删除专辑");
        }

        [HttpPatch]
        public ActionResult UpdateAlbum(int id, [FromForm] CreateAlbumDto albumDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest($"存在非法数据，更新失败");
            }
            _albumService.UpdateAlbum(id, albumDto);
            return Ok("成功更新专辑");
        }

        [HttpGet]
        public ActionResult<ICollection<AlbumInfoDto>> GetAllAlbums()
        {
            var albums = _albumService.GetAllAlbums();
            return Ok(albums);
        }

        [HttpGet]
        public ActionResult<AlbumInfoDto> GetAlbumById(int id)
        {
            var album = _albumService.GetAlbumById(id);
            if(album != null)
            {
                return Ok(album);
            }
            return NotFound("未找到对应专辑");
        }
    }
}
