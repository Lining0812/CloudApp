using CloudApp.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace CloudApp.Service
{
    public class StorageProvider : IStorageProvider
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _albumpath;
        private readonly string _concertpath;
        private readonly string _trackpath;

        public StorageProvider(IWebHostEnvironment env)
        {
            _env = env;
            _albumpath = Path.Combine(env.WebRootPath, "images", "albums");
            _concertpath = Path.Combine(env.WebRootPath, "images", "concerts");
            _trackpath = Path.Combine(env.WebRootPath, "images", "tracks");
            
            Directory.CreateDirectory(_albumpath);
            Directory.CreateDirectory(_concertpath);
            Directory.CreateDirectory(_trackpath);
        }

        public string SaveImage(IFormFile image, string folderName)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            var filePath = Path.Combine(folderName, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            string folder = new DirectoryInfo(folderName).Name;
            return $"{folder}/{fileName}";
        }

        public string SaveAlbumImage(IFormFile image)
        {
            if (image == null)
            {
                return "albums/default_cover.jpg";
            }
            return SaveImage(image, _albumpath);
        }

        public string SaveConcertImage(IFormFile image)
        {
            if(image == null)
            {
                return "concerts/default_cover.jpg";
            }
            return SaveImage(image, _concertpath);
        }

        public string SaveTrackImage(IFormFile image)
        {
            if (image == null)
            {
                return "tracks/default_cover.jpg";
            }
            return SaveImage(image, _trackpath);
        }

        public string SaveFile(IFormFile file, string filepath)
        {
            throw new NotImplementedException();
        }

        public void DeleteFile(string filepath)
        {
            throw new NotImplementedException();
        }

        public void UpdateFile(string filepath)
        {
            throw new NotImplementedException();
        }
    }
}
