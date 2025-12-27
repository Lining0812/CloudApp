using CloudApp.Core.Dtos;
using CloudApp.Core.Interfaces;
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
            if (ModelState.IsValid)
            {
                _albumService.AddAlbum(albumDto);
                return Ok("成功新增专辑");
            }
            return BadRequest($"存在非法数据，添加失败");
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
            try
            {
                var album = _albumService.GetAlbumById(id);
                return Ok(album);
            }
            catch (ArgumentException ex) when (ex.Message == "查询的专辑不存在")
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult DeleteAlbum(int id)
        {
            try
            {
                _albumService.DeleteAlbum(id);
                return Ok("Successful DeleteAlbum");
            }
            catch (ArgumentException ex) when (ex.Message == "专辑不存在")
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
