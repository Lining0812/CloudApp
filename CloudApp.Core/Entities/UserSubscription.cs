using CloudApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApp.Core.Entities
{
    /// <summary>
    /// 用户订阅
    /// </summary>
    public class UserSubscription : BaseEntity
    {
        public int UserId { get; set; }
        public int TargetId { get; set; }
        public SubscriptionTargetType TargetType { get; set; }
    }
}
