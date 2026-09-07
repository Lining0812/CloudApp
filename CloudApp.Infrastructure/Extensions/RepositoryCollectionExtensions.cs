using CloudApp.Core.Confige;
using CloudApp.Core.Interfaces.Repositories;
using CloudApp.Core.Interfaces.Services;
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
        /// 统一注册仓储与基础设施服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = GetConnectionString(configuration);
            var dbType = GetDbType(configuration);

            // 添加数据库上下文
            services.AddDbContext<MyDBContext>(opt =>
            {
                if (dbType == "mysql")
                {
                    opt.UseMySQL(connectionString);
                }
                else
                {
                    opt.UseSqlServer(connectionString);
                }
            });

            // 添加通用仓储
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            // 添加特定仓储
            services.AddScoped<IAlbumRepository, AlbumRepository>();
            services.AddScoped<IConcertRepository, ConcertRepository>();
            services.AddScoped<ITrackRepository, TrackRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();

            // 添加存储配置
            services.Configure<StorageOptions>(configuration.GetSection("Storage"));

            // 添加存储提供者
            services.AddScoped<IStorageProvider, LocalStorageProvider>();

            return services;
        }

        /// <summary>
        /// 注册权限服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = GetConnectionString(configuration);
            var dbType = GetDbType(configuration);

            // 添加数据库上下文
            services.AddDbContext<MyDBContext>(opt =>
            {
                if (dbType == "mysql")
                {
                    opt.UseMySQL(connectionString);
                }
                else
                {
                    opt.UseSqlServer(connectionString);
                }
            });
            services.AddDataProtection();

            services.AddIdentityCore<AppUser>(opt =>
            {
                // 配置用户名可以有中文字符
                //opt.User.AllowedUserNameCharacters = null;
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

        public static IServiceCollection AddCookie(this IServiceCollection services)
        {
            return services;
        }

        private static string GetDbType(IConfiguration configuration)
        {
            var dbType = Environment.GetEnvironmentVariable("DB_TYPE")?.Trim().ToLowerInvariant();
            if (string.IsNullOrEmpty(dbType))
            {
                dbType = configuration.GetConnectionString("DbType");
            }
            return dbType;
        }

        private static string GetConnectionString(IConfiguration configuration)
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbUser = Environment.GetEnvironmentVariable("DB_USER");
            var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
            var dbType = GetDbType(configuration);

            if (!string.IsNullOrEmpty(dbHost) && !string.IsNullOrEmpty(dbName) && !string.IsNullOrEmpty(dbUser) && !string.IsNullOrEmpty(dbPassword))
            {
                if (dbType == "mysql")
                {
                    var port = string.IsNullOrEmpty(dbPort) ? "3306" : dbPort; // 默认端口为3306
                    return $"Server={dbHost};Port={port};Database={dbName};User={dbUser};Password={dbPassword};";
                }
                else
                {
                    var port = string.IsNullOrEmpty(dbPort) ? "1433" : dbPort; // 默认端口为1433
                    return $"Server={dbHost},{port};Database={dbName};User Id={dbUser};Password={dbPassword};TrustServerCertificate=True;";
                }
            }

            return configuration.GetConnectionString("DefaultConnection");
        }
    }
}
