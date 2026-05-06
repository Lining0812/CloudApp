using CloudApp.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudApp.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpPost]
        public ActionResult<string> CreateSchedule()
        {
            _scheduleService.CreateSchedule();
            return Ok("创建行程成功");
        }

        [HttpGet]
        public ActionResult<List<string>> GetSchedules()
        {
            var schedules = _scheduleService.GetSchedules();
            return Ok(schedules);
        }
    }
}
