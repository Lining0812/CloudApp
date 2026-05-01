using CloudApp.Core.Dtos.WeChat;

namespace CloudApp.Core.Interfaces
{
    public interface IWeChatService
    {
        /// <summary>
        /// 生成微信授权跳转URL（前端用这个地址让用户登录）
        /// </summary>
        /// <returns></returns>
        string GetWeChatAuthorizeUrl();

        /// <summary>
        /// 获取微信访问令牌
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<WeChatAccessTokenResponse?> GetAccessTokenAsync(string code);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        Task<WeChatUserInfoResponse?> GetUserInfoAsync(string accessToken, string openid);

        /// <summary>
        /// 小程序登录 - 通过 code 换取 session
        /// </summary>
        Task<WeChatCode2SessionResponse?> Code2SessionAsync(string code);
    }
}
