using CloudApp.Core.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CloudApp.Application.Extensions
{
    /// <summary>
    /// 服务注入
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 统一注册基础服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // 添加业务服务
            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<ITrackService, TrackService>();
            services.AddScoped<IConcertService, ConcertService>();
            services.AddScoped<IMediaService, MediaService>();
            services.AddScoped<IFileService, FileService>();

            return services;
        }
    }
}
