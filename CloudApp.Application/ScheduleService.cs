using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using CloudApp.Core.Enums;
using CloudApp.Core.Extensions;
using CloudApp.Core.Interfaces.Repositories;
using CloudApp.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace CloudApp.Application
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _repository;
        private readonly ILogger<ScheduleService> _logger;

        public ScheduleService(IScheduleRepository repository, ILogger<ScheduleService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public void CreateSchedule()
        {
            var schedule = new Schedule() {
                Title = "New Schedule",
                Artist = "Artist Name",
                StartTime = new DateOnly(2026, 5, 5),
                EndTime = new DateOnly(2026, 5, 5),
                Type = ScheduleType.Concert,
                Status = ScheduleStatus.Scheduled,
                IsPublic = true
            };
            _repository.Add(schedule);
            _repository.SaveChange();
        }

        public string CreateSchedule(ScheduleCreateRequest request)
        {
            var schedule = request.ToEntity();
            _repository.Add(schedule);
            _repository.SaveChange();
            return schedule.Title;
        }

        public List<string> GetSchedules()
        {
            var schedules = _repository.GetAll();
            return schedules.Select(s => s.Title).ToList();
        }
    }
}
