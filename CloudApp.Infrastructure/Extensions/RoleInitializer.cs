using CloudApp.Core.Enums;
using CloudApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CloudApp.Infrastructure.Extensions
{
    /// <summary>
    /// 创建角色的初始化器
    /// </summary>
    public static class RoleInitializer
    {
        public static async Task InitializerRoleAsync(this IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

            foreach (var roleName in Enum.GetNames(typeof(RoleType)))
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new AppRole { Name = roleName };
                    var result = await roleManager.CreateAsync(role);
                }
            }
        }
    }
}
