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
        /// <param name="accessToken"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        Task<WeChatUserInfoResponse?> GetUserInfoAsync(string accessToken, string openid);
    }
}
