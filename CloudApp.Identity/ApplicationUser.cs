using Microsoft.AspNetCore.Identity;

namespace CloudApp.Identity
{
    public class ApplicationUser : IdentityUser
    {
        // 可以添加自定义用户属性
        public string? FullName { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}