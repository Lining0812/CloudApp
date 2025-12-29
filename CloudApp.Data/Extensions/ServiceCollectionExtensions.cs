using CloudApp.Core.Interfaces.Repositories;
using CloudApp.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CloudApp.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
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
    }
}