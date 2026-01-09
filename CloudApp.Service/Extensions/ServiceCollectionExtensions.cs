using CloudApp.Core.Interfaces.Services;
using CloudApp.Service;
using Microsoft.Extensions.DependencyInjection;

namespace CloudApp.Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // 添加业务服务
            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<ITrackService, TrackService>();
            services.AddScoped<IConcertService, ConcertService>();

            // 添加存储提供者
            services.AddScoped<IStorageProvider, LocalStorageProvider>();

            return services;
        }
    }
}