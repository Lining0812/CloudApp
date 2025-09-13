using CloudEFCore;
using CloudWebApi.Models;
using CloudWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudWebApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class SingleController : ControllerBase
    {
        private readonly SingleRepository _userRepository;
        public SingleController(SingleRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public SingleSongDto GetAllSingles()
        {
            return _userRepository.GetAllSinges();
        }
    }
}
