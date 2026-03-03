using CloudApp.Core.Interfaces.Repositories;
using CloudApp.Infrastructure.Identity;
using CloudApp.Infrastructure.Repositories;
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
            //services.AddDbContext<ArDbContext>();
            return services;
        }
    }
}