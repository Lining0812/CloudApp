using Microsoft.AspNetCore.Http;

namespace CloudApp.Service.Utils
{
    public static class FilePathProvider
    {

        private static string FilePath(string path, IFormFile file)
        {

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName).ToLower()}";
            var filePath = Path.Combine(path, fileName);

            return filePath;
        }
    }
}
    