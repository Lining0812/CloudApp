using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApp.Core.Extensions
{
    public static class ScheduleExtension
    {
        public static Schedule ToEntity(this ScheduleCreateRequest request)
        {
            return new Schedule
            {
                Title = request.Title,
                Description = request.Description,
                Artist = request.Artist,
                Location = request.Location,
                StartTime = request.StartTime,
                EndTime = request.Endtime,
                Type = request.Type,
                Status = request.Status,
                IsPublic = request.IsPublic
            };
        }
    }
}
