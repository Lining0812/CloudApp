using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApp.Core.Dtos.Account
{
    public class UpdateUserProfileDto
    {
        public string? NickName { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
