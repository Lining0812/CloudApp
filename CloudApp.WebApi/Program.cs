using CloudApp.Core.Entities;
using CloudApp.Core.Interfaces;
using CloudApp.Data;
using CloudApp.Data.Repositories;
using CloudApp.Service;
using Microsoft.EntityFrameworkCore;

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
            builder.Services.AddScoped<IRepository<Track>, TrackRepository>();
            builder.Services.AddScoped<IRepository<Album>, AlbumRepository>();

            builder.Services.AddScoped<IAlbumService, AlbumService>();
            builder.Services.AddScoped<ITrackService, TrackService>();
            builder.Services.AddScoped<IFileService>(provider => 
                new FileService(builder.Environment.WebRootPath));

            builder.Services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:5174")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseStaticFiles();

            app.Run();
        }
    }
}
