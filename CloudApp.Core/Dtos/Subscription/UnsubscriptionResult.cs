using CloudApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApp.Core.Dtos.Subscription
{
    /// <summary>
    /// 退订结果
    /// </summary>
    public class UnsubscriptionResult
    {
        public bool Success { get; set; }

        /// <summary>本次调用是否真的改变过订阅状态（false = 本来就没订阅）</summary>
        public bool WasSubscribed { get; set; }

        public int TargetId { get; set; }

        public SubscriptionTargetType TargetType { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}
