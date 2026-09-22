using CloudApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApp.Core.Dtos.Subscription
{
    /// <summary>
    /// 订阅结果
    /// </summary>
    public class SubscriptionResult
    {
        public bool Success { get; set; }
        public bool AlreadySubscribed { get; set; }
        public int TargetId { get; set; }
        public SubscriptionTargetType TargetType { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
