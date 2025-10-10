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
        private readonly IFileService _fileService;

        public AlbumController(IAlbumService albumService, IFileService fileService)
        {
            _albumService = albumService;
            _fileService = fileService;
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
            catch (ArgumentException ex) when (ex.Message == "专辑不存在")
            {
                return NotFound(ex.Message);
            }
        }
        
        [HttpPost]
        public ActionResult<int> AddAlbum([FromForm]CreateAlbumDto albumDto)
        {
            if (ModelState.IsValid)
            {
                int newid = _albumService.AddAlbum(albumDto);
                return Ok(newid);
            }
            return BadRequest("Invalid data.");
        }

        //[HttpPost]
        //public ActionResult AddAlbum([FromForm] CreateAlbumDto albumDto, IFormFile coverImage)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // 如果提供了封面图片，使用FileService上传
        //        if (coverImage != null && coverImage.Length > 0)
        //        {
        //            try
        //            {
        //                // 上传图片并获取URL
        //                string imageUrl = _fileService.UploadFile(coverImage, "images/albums");
        //                albumDto.CoverImageUrl = imageUrl;
        //            }
        //            catch (Exception ex)
        //            {
        //                return BadRequest("图片上传失败: " + ex.Message);
        //            }
        //        }

        //        _albumService.AddAlbum(albumDto);
        //        return Ok("Successful AddAlbum");
        //    }
        //    return BadRequest("Invalid data.");
        //}

        //[HttpPost]
        //public ActionResult UpdateAlbum(int id, CreateAlbumDto albumDto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _albumService.UpdateAlbum(id, albumDto);
        //        return Ok("Successful UpdateAlbum");
        //    }
        //    return BadRequest("Invalid data.");
        //}

        //[HttpDelete]
        //public ActionResult DeleteAlbum(int id)
        //{
        //    try
        //    {
        //        _albumService.DeleteAlbum(id);
        //        return Ok("Successful DeleteAlbum");
        //    }
        //    catch (ArgumentException ex) when (ex.Message == "专辑不存在")
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
