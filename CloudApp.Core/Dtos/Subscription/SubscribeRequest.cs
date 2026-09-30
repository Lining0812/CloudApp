using CloudApp.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CloudApp.Core.Dtos.Subscription
{
    /// <summary>
    /// 订阅请求
    /// </summary>
    public class SubscribeRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "TargetId 必须为正整数")]
        public int TargetId { get; set; }
        [EnumDataType(typeof(SubscriptionTargetType), ErrorMessage = "TargetType 取值非法")]
        public SubscriptionTargetType TargetType { get; set; }
    }
}
