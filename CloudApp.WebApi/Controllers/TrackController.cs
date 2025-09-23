﻿using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using CloudApp.Core.Interface;
using CloudApp.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudApp.WebApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly ITrackService _trackService;
        public TrackController(ITrackService trackService)
        {
            _trackService = trackService;
        }

        [HttpPost]
        public ActionResult AddTrack(CreateTrackDto model)
        {
            if (ModelState.IsValid)
            {
                _trackService.AddTrack(model);
                return Ok("Successfull AddTrack");
            }
            return BadRequest("Invalid data.");
        }
        [HttpGet]
        public ActionResult<ICollection<Track>> GetAllTracks()
        {
            var tracks = _trackService.GetAllTracks();
            return Ok(tracks);
        }
    }
}
