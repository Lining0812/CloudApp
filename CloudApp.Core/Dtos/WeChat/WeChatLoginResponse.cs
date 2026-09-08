using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApp.Core.Dtos.WeChat
{
    public class WeChatLoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public UserInfoDto UserInfo { get; set; } = new ();
    }

    public class UserInfoDto
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? AvatarUrl { get; set; }
        public List<string> Roles { get; set; } = new();

    }
}
