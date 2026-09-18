namespace CloudApp.Core.Dtos.WeChat
{
    public class WeChatLoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public UserInfoDto UserInfo { get; set; } = new ();
        public bool IsNewUser { get; set; }
    }

    public class UserInfoDto
    {
        public string Id { get; set; } = string.Empty;
        public string? NickName { get; set; }
        public string? AvatarUrl { get; set; }
        public string? PhoneNumber { get; set; }
        public List<string> Roles { get; set; } = new();

    }
}
