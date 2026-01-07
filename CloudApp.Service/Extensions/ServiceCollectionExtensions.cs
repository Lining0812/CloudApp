using CloudApp.Core.Interfaces.Services;
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

            return services;
        }
    }
}