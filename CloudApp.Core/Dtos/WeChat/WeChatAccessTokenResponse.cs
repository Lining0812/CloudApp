using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApp.Core.Dtos.WeChat
{
    public class WeChatAccessTokenResponse
    {
        public string access_token { get; set; }
        public string openid { get; set; }
        public string unionid { get; set; }
        public int errcode { get; set; }
        public string errmsg { get; set; }
    }
}
