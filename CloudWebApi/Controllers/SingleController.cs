using CloudEFCore;
using CloudEFCore.Models;
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
        private readonly IRepository<SingleSong> _singlesongRepository;
        public SingleController(IRepository<SingleSong> singlesongRepository)
        {
            _singlesongRepository = singlesongRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var singles = _singlesongRepository.GetAll();
            return Ok(singles);
        }
    }
}
