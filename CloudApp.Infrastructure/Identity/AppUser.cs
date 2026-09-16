using Microsoft.AspNetCore.Identity;

namespace CloudApp.Infrastructure.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public string? WeChatOpenId { get; set; }
        public string? WeChatUnionId { get; set; }
        public string? NickName { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
