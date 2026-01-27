using CloudApp.Core.Entities;
using CloudApp.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace CloudApp.Core.Interfaces.Services;

public interface IMediaService
{
    MediaResource AddMedia(IFormFile file, MediaType mediaType);
    
    void DeleteMedia(int id);
    
    MediaResource UpdateResource(int id, IFormFile file);
}