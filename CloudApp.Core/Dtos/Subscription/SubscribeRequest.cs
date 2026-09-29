using CloudApp.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CloudApp.Core.Dtos.Subscription
{
    public class SubscribeRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "TargetId 必须为正整数")]
        public int TargetId { get; set; }
        [EnumDataType(typeof(SubscriptionTargetType), ErrorMessage = "TargetType 取值非法")]
        public SubscriptionTargetType TargetType { get; set; }
    }
}
