using CloudApp.Core.Dtos.WeChat;
using CloudApp.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace CloudApp.Application
{
    public class WeChatService : IWeChatService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public WeChatService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public string GetWeChatAuthorizeUrl()
        {
            var appId = _configuration["WeChat:AppId"];
            var redirectUri = _configuration["WeChat:RedirectUri"];
            return $"https://open.weixin.qq.com/connect/oauth2/authorize"
                + $"?appid={appId}"
                + $"&redirect_uri={redirectUri}"
                + $"&response_type=code"
                + $"&scope=snsapi_userinfo"
                + $"&state=STATE#wechat_redirect";
        }

        public async Task<WeChatAccessTokenResponse?> GetAccessTokenAsync(string code)
        {
            var appId = _configuration["WeChat:AppId"];
            var secret = _configuration["WeChat:AppSecret"];
            var url = $"https://api.weixin.qq.com/sns/oauth2/access_token"
                + $"?appid={appId}"
                + $"&secret={secret}"
                + $"&code={code}"
                + $"&grant_type=authorization_code";

            return await _httpClient.GetFromJsonAsync<WeChatAccessTokenResponse>(url);
        }

        // 获取微信用户信息（昵称、头像）
        public async Task<WeChatUserInfoResponse?> GetUserInfoAsync(string accessToken, string openid)
        {
            var url = $"https://api.weixin.qq.com/sns/userinfo"
                + $"?access_token={accessToken}"
                + $"&openid={openid}"
                + $"&lang=zh_CN";

            return await _httpClient.GetFromJsonAsync<WeChatUserInfoResponse>(url);
        }
    }
}
