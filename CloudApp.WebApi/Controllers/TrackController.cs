using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using CloudApp.Service.Interfaces;
using CloudApp.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudApp.WebApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly TrackService _trackService;
        public TrackController(TrackService trackService)
        {
            _trackService = trackService;
        }

        [HttpPost]
        public ActionResult Add(CreateTrackDto model)
        {
            if (ModelState.IsValid)
            {
                _trackService.AddTrack(model);

                return Ok("Successfull Add");
            }
            return BadRequest("Invalid data.");
        }
        [HttpGet]
        public ActionResult<IEnumerable<Track>> GetAll()
        {
            var tracks = _trackService.GetAllTracks();
            return Ok(tracks);
        }
    }
}
