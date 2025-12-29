// 在CloudApp.Service中获取应用程序目录的代码示例

using CloudApp.Core.Interfaces.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CloudApp.Service
{
    // 1. 首先，在Service类的构造函数中注入IWebHostEnvironment或IHostEnvironment
    public class ExampleService : IExampleService
    {
        private readonly IExampleRepository _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHostEnvironment _hostEnvironment;
        
        // 构造函数注入
        public ExampleService(
            IExampleRepository repository,
            IWebHostEnvironment webHostEnvironment,
            IHostEnvironment hostEnvironment)
        {
            _repository = repository;
            _webHostEnvironment = webHostEnvironment;
            _hostEnvironment = hostEnvironment;
        }
        
        // 2. 使用注入的服务获取各种目录路径
        public void GetDirectories()
        {
            // 获取Web根目录 (wwwroot目录)
            string webRootPath = _webHostEnvironment.WebRootPath;
            
            // 获取内容根目录 (应用程序根目录)
            string contentRootPath = _hostEnvironment.ContentRootPath;
            
            // 示例：结合使用路径
            string imagesPath = Path.Combine(webRootPath, "images");
            string dataPath = Path.Combine(contentRootPath, "Data");
            
            // 3. 检查目录是否存在，不存在则创建
            if (!Directory.Exists(imagesPath))
            {
                Directory.CreateDirectory(imagesPath);
            }
        }
    }
    
    // 4. 确保在ServiceCollectionExtensions中注册服务
    // 在CloudApp.Service.Extensions.ServiceCollectionExtensions.cs中添加：
    /*
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        // 添加业务服务
        services.AddScoped<IExampleService, ExampleService>();
        // 其他服务...
        
        return services;
    }
    */
    
    // 5. 在Program.cs中，ASP.NET Core会自动注册IWebHostEnvironment和IHostEnvironment
    // 无需额外配置
}

// 替代方案：如果只需要获取应用程序根目录，也可以使用Assembly
using System.Reflection;

public class AlternativeExampleService
{
    public string GetApplicationDirectory()
    {
        // 获取当前程序集的目录
        string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        return assemblyPath;
    }
}