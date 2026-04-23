using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApp.Core.Dtos.WeChat
{
    public class WeChatUserInfoResponse
    {
        public string openid { get; set; }
        public string nickname { get; set; }
        public string headimgurl { get; set; }
        public string unionid { get; set; }
        public int errcode { get; set; }
        public string errmsg { get; set; }
    }
}
