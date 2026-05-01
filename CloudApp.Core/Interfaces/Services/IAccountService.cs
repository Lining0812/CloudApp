using CloudApp.Core.Dtos.Account;

namespace CloudApp.Core.Interfaces.Services
{
    public interface IAccountService
    {
        Task<string> CreateAdminUserAsync();

        /// <summary>
        /// 用户注册
        /// </summary>
        Task<string> RegisterAsync(RegisterRequest request);

        /// <summary>
        /// 仅使用手机号注册
        /// </summary>
        Task<string> RegisterByPhoneAsync(string phoneNumber, string password);

        /// <summary>
        /// 生成密码重置令牌
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<string> GeneratePasswordResetTokenAsync(string userName);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="token"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task<string> ResetPasswordAsync(string userName, string token, string newPassword);
        
        Task<string> LoginAsync(string userName, string password);

        string FindByIdAsync(string id);

        /// <summary>
        /// 微信小程序登录
        /// </summary>
        Task<string> WeChatLoginAsync(string code);
    }
}
