using CloudApp.Core.Entities;
using CloudApp.Service.Interfaces;
using CloudWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudApp.WebApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly IRepository<Track> _trackRepository;
        public TrackController(IRepository<Track> trackRepository)
        {
            _trackRepository = trackRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var singles = _trackRepository.GetAll();
            return Ok(singles);
        }
        [HttpPost]
        public IActionResult Add(Track model)
        {
            if (ModelState.IsValid)
            {
                var track = new Track
                {
                    Title = model.Title
                };
                _trackRepository.Add(track);
                return Ok("Successfull Add");
            }
            return BadRequest("Invalid data.");
        }
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var track = _trackRepository.GetById(id);
            if (track == null)
            {
                return NotFound();
            }
            return Ok(track);
        }
    }
}
