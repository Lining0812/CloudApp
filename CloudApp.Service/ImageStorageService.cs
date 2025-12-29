using CloudApp.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace CloudApp.Service
{
    public class ImageStorageService : IImageStorageService
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _albumpath;
        private readonly string _concertpath;
        private readonly string _trackpath;

        public ImageStorageService(IWebHostEnvironment env)
        {
            _env = env;
            _albumpath = Path.Combine(env.WebRootPath, "images", "albums");
            _concertpath = Path.Combine(env.WebRootPath, "images", "concerts");
            _trackpath = Path.Combine(env.WebRootPath, "images", "tracks");
            
            Directory.CreateDirectory(_albumpath);
            Directory.CreateDirectory(_concertpath);
            Directory.CreateDirectory(_trackpath);
        }

        private string SaveImage(IFormFile image, string folderName)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            var filePath = Path.Combine(folderName, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            return Path.Combine("concerts", fileName);
        }

        public string SaveAlbumImage(IFormFile image)
        {
            if (image == null)
            {
                // 返回null也可以
                return "albums\\default_cover.jpg";
            }
            return SaveImage(image, _albumpath);
        }

        public string SaveConcertImage(IFormFile image)
        {
            if(image == null)
            {
                return "concerts\\default_cover.jpg";
            }
            return SaveImage(image, _concertpath);
        }

        public string SaveTrackImage(IFormFile image)
        {
            if (image == null)
            {
                return "tracks\\default_cover.jpg";
            }
            return SaveImage(image, _trackpath);
        }

        public (Stream stream, string contentType) GetImage(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
            {
                throw new ArgumentException("图片路径不能为空", nameof(imagePath));
            }

            string fullPath = Path.Combine(_env.WebRootPath, imagePath);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException("图片不存在", imagePath);

            var stream = File.OpenRead(fullPath);
            return (stream, GetContentType(Path.GetExtension(imagePath)));
        }

        // 根据文件扩展名获取MIME类型
        private string GetContentType(string extension)
        {
            return extension.ToLower() switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream"
            };
        }
    }
}
