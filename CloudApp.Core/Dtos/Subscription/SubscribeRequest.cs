using CloudApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApp.Core.Dtos.Subscription
{
    public class SubscribeRequest
    {
        public int TargetId { get; set; }
        public SubscriptionTargetType TargetType { get; set; }
    }
}
