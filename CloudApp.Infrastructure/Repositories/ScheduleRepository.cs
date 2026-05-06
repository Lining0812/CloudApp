using CloudApp.Core.Entities;
using CloudApp.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApp.Infrastructure.Repositories
{
    public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(MyDBContext context,ILogger<ScheduleRepository> logger)
            : base(context, logger)
        {
        }
    }
}
