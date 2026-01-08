using CloudApp.Core.Interfaces.Services;
using CloudApp.Service.Utils;
using Microsoft.AspNetCore.Http;

namespace CloudApp.Service
{
    public class LocalStorageProvider : IStorageProvider
    {
        public string SaveFile(IFormFile file, string type)
        {
            try
            {
                string filePath = FilePathProvider.GenerateFullFilePath(file, type);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return "Success";
            }
            catch(Exception ex)
            {
                Console.WriteLine($"保存失败: {ex.Message}");
                throw;
            }
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
