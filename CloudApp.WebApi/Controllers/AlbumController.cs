using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using CloudApp.Service.Interfaces;
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
    }
}
