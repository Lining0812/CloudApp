using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApp.Core.Dtos.WeChat
{
    public class WeChatRegisterRequest
    {
        public string? RegisterTicket { get; set; } = "";
        public string? NickName { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
