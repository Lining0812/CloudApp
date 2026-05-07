using CloudApp.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApp.Core.Interfaces.Services
{
    public interface IScheduleService
    {
        void CreateSchedule();

        string CreateSchedule(ScheduleCreateRequest request);

        List<string> GetSchedules();
    }
}
