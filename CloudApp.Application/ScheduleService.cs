using CloudApp.Core.Entities;
using CloudApp.Core.Enums;
using CloudApp.Core.Interfaces.Repositories;
using CloudApp.Core.Interfaces.Services;
using CloudApp.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

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
                StartTime = DateTime.Now,
                Type = ScheduleType.Concert,
                Status = ScheduleStatus.Scheduled,
                IsPublic = true
            };
            _repository.Add(schedule);
            _repository.SaveChange();
        }
    }
}
