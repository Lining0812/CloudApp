using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using CloudApp.Core.Interface;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        public ActionResult<ICollection<Album>> GetAllAlbum()
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
            catch (ArgumentException ex) when (ex.Message == "专辑不存在")
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult AddAlbum(CreateAlbumDto albumDto)
        {
            if (ModelState.IsValid)
            {
                _albumService.AddAlbum(albumDto);
                return Ok("Successful AddAlbum");
            }
            return BadRequest("Invalid data.");
        }

        [HttpPost]
        public ActionResult UpdateAlbum(int id, CreateAlbumDto albumDto)
        {
            if (ModelState.IsValid)
            {
                _albumService.UpdateAlbum(id, albumDto);
                return Ok("Successful UpdateAlbum");
            }
            return BadRequest("Invalid data.");
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
