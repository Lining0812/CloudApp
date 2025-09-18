using CloudApp.Core.Entities;
using CloudApp.Service.Interfaces;
using CloudApp.Service.Services;
using CloudWebApi.Models;
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
        public IActionResult Add(CreatTrackDto model)
        {
            if (ModelState.IsValid)
            {
                _trackService.AddTrack(model);

                return Ok("Successfull Add");
            }
            return BadRequest("Invalid data.");
        }
    }
}
