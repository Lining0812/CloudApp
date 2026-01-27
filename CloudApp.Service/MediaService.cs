using CloudApp.Core.Entities;
using CloudApp.Core.Enums;
using CloudApp.Core.Interfaces.Repositories;
using CloudApp.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CloudApp.Service;

public class MediaService : IMediaService
{
    private readonly IRepository<MediaResource> _mediaRepository;
    private readonly IStorageProvider _storageProvider;
    private readonly ILogger<MediaService> _logger;
    
    public MediaService(
        IRepository<MediaResource> mediaRepository,
        IStorageProvider storageProvider, 
        ILogger<MediaService> logger)
    {
        _mediaRepository = mediaRepository;
        _storageProvider = storageProvider;
        _logger = logger;
    }
    
    public MediaResource AddMedia(IFormFile file,MediaType mediaType)
    {
        throw new NotImplementedException();
    }

    public void DeleteMedia(int id)
    {
        throw new NotImplementedException();
    }

    public MediaResource UpdateResource(int id, IFormFile file)
    {
        throw new NotImplementedException();
    }
}