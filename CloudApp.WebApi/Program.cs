using CloudApp.Application;
using CloudApp.Application.Extensions;
using CloudApp.Core.Confige;
using CloudApp.Core.Interfaces;
using CloudApp.Infrastructure.Extensions;
using CloudApp.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CloudApp.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // 添加数据层服务
            builder.Services.AddDataServices(builder.Configuration);
            builder.Services.AddIdentityService(builder.Configuration);

            // 添加业务逻辑服务
            builder.Services.AddServices();

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

            builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("Jwt"));
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    var jwtSetting = builder.Configuration.GetSection("Jwt").Get<JwtSetting>();
                    byte[] key = Encoding.UTF8.GetBytes(jwtSetting.SecKey);
                    var secKey = new SymmetricSecurityKey(key);
                    opt.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = secKey
                    };
                });

            // 注入HttpClient和微信服务
            //builder.Services.AddHttpClient();
            //builder.Services.AddScoped<IWeChatService, WeChatService>();

            var app = builder.Build();

            // 初始化角色
            await app.Services.InitializerRoleAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // 添加全局异常处理中间件（应该放在最前面）
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.UseStaticFiles();

            app.Run();
        }
    }
}
