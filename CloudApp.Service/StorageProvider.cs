using CloudApp.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace CloudApp.Service
{
    public class LocalStorageProvider : IStorageProvider
    {

        public LocalStorageProvider()
        {
        }

        public string SaveFile(IFormFile file, string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            // 返回相对路径
            return "Success";
        }

        public void DeleteFile(string filepath)
        {
            throw new NotImplementedException();
        }

        public void UpdateFile(string filepath)
        {
            throw new NotImplementedException();
        }

        public Stream ReadFile()
        {
            throw new NotImplementedException();
        }
    }
}
