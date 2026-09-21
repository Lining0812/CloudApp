using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApp.Core.Dtos.CheckIn
{
    public class CheckInStatus
    {
        public bool CheckedInToday { get; set; }
        public int Streak { get; set; }
        public int TotalDays { get; set; }
        public DateOnly Today { get; set; }
        public DateOnly? LastCheckInDate { get; set; }
    }
}
