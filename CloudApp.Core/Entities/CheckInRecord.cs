using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApp.Core.Entities
{
    public class CheckInRecord : BaseEntity
    {
        public required string OpenId { get; set; }
        public DateOnly CheckInDate { get; set; }
    }
}
