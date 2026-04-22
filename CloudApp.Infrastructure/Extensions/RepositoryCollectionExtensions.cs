using CloudApp.Core.Interfaces.Repositories;
using CloudApp.Infrastructure.Identity;
using CloudApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CloudApp.Infrastructure.Extensions
{
    public static class RepositoryCollectionExtensions
    {
        /// <summary>
        /// 统一注册仓储服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 添加数据库上下文
            services.AddDbContext<MyDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // 添加通用仓储
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            // 添加特定仓储
            services.AddScoped<IAlbumRepository, AlbumRepository>();
            services.AddScoped<IConcertRepository, ConcertRepository>();
            services.AddScoped<ITrackRepository, TrackRepository>();

            services.AddDataProtection();
            services.AddIdentityCore<AppUser>(opt =>
            {
                // 密码配置
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredLength = 6;
                opt.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                opt.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            })
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<MyDBContext>()
                .AddDefaultTokenProviders()
                .AddUserManager<UserManager<AppUser>>()
                .AddRoleManager<RoleManager<AppRole>>();

            return services;
        }

        /// <summary>
        /// 注册权限服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        //public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration configuration)
        //{
        //    // 添加数据库上下文
        //    services.AddDbContext<ArDBContext>(opt => { opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")); });
        //    services.AddDataProtection();

        //    services.AddIdentityCore<AppUser>(opt =>
        //    {
        //        // 密码配置
        //        opt.Password.RequireDigit = false;
        //        opt.Password.RequireLowercase = false;
        //        opt.Password.RequireNonAlphanumeric = false;
        //        opt.Password.RequireUppercase = false;
        //        opt.Password.RequiredLength = 6;
        //        opt.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
        //        opt.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
        //    })
        //        .AddEntityFrameworkStores<MyDBContext>()
        //        .AddDefaultTokenProviders()
        //        .AddUserManager<UserManager<AppUser>>()
        //        .AddRoleManager<RoleManager<AppRole>>();

        //    return services;
        //}
    }
}