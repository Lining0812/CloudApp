
using CloudApp.Core.Entities;
using CloudApp.Data;
using CloudApp.Data.Repository;
using CloudApp.Service.Interfaces;
using CloudApp.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CloudApp.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<MyDBContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddScoped(typeof(IRepository<>),typeof(BaseRepository<>));
            builder.Services.AddScoped<IRepository<Track>,TrackRepository>();
            builder.Services.AddScoped<IRepository<Album>, AlbumRepository>();

            builder.Services.AddScoped<IAlbumService, AlbumSevice>();
            builder.Services.AddScoped<ITrackService, TrackService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
