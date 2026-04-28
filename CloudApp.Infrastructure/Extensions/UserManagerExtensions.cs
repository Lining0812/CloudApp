using CloudApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CloudApp.Infrastructure.Extensions
{
    /// <summary>
    /// identity.user的扩展方法
    /// </summary>
    public static class UserManagerExtensions
    {
        /// <summary>
        /// 根据手机号返回用户实例
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static async Task<AppUser?> FindByPhoneNumberAsync(this UserManager<AppUser> userManager, string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber)) return null;

            var users = userManager.Users;
            return await users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        }

        /// <summary>
        /// 判断手机号是否已注册
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static async Task<bool> IsPhoneNumberRegisteredAsync(this UserManager<AppUser> userManager, string phoneNumber)
        {
            return await userManager.FindByPhoneNumberAsync(phoneNumber) != null;
        }
    }
}
