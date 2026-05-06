using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApp.Core.Interfaces.Services
{
    public interface IScheduleService
    {
        void CreateSchedule();

        List<string> GetSchedules();
    }
}
