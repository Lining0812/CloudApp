using CloudApp.Core.Interfaces.Services;
using CloudApp.Service;
using Microsoft.Extensions.DependencyInjection;

namespace CloudApp.Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            // 添加业务服务
            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<ITrackService, TrackService>();
            services.AddScoped<IConcertService, ConcertService>();

            services.AddScoped<IImageStorageService, ImageStorageService>();

            return services;
        }
    }
}