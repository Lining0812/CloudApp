using Microsoft.AspNetCore.Http;

namespace CloudApp.Core.Interface
{
    public interface IFileService
    {
        string UploadFile(IFormFile file, string UploadDirectory);
        bool ValidateFile(IFormFile file, string[] allowedExtensions, long maxFileSizeBytes);
    }
}
