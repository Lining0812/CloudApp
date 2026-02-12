using CloudApp.Core.Interfaces.Services;
using CloudApp.Service;
using Microsoft.Extensions.DependencyInjection;

namespace CloudApp.Service.Extensions
{
    /// <summary>
    /// 服务注入
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // 添加业务服务
            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<ITrackService, TrackService>();
            services.AddScoped<IConcertService, ConcertService>();
            services.AddScoped<IMediaService, MediaService>();

            // 添加存储提供者
            services.AddScoped<IStorageProvider, LocalStorageProvider>();

            return services;
        }
    }
}