using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace CloudApp.Core.Interfaces.Services
{
    public interface IFileService
    {
        string SaveFile(IFormFile file, string filepath);
        void DeleteFile(string filepath);
        void UpdateFile(string filepath);
    }
}
