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
    private readonly IRepository<MediaRelation> _relationRepository;
    private readonly IStorageProvider _storageProvider;
    private readonly ILogger<MediaService> _logger;
    
    public MediaService(
        IRepository<MediaResource> mediaRepository,
        IRepository<MediaRelation> relationRepository,
        IStorageProvider storageProvider, 
        ILogger<MediaService> logger)
    {
        _mediaRepository = mediaRepository;
        _relationRepository = relationRepository;
        _storageProvider = storageProvider;
        _logger = logger;
    }

    public MediaResource AddMedia(IFormFile file, MediaType mediaType)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("文件不能为空");
        }

        // 后续优化：关系创建后再保存
        string filePath = _storageProvider.SaveFile(file, Entype.Album);

        var mediaResource = new MediaResource
        {
            FileName = file.FileName,
            FilePath = filePath,
            ContentType = file.ContentType,
            MediaType = mediaType,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        _mediaRepository.Add(mediaResource);
        _mediaRepository.SaveChange();

        return mediaResource;
    }

    public void AddMediaWithRelation(IFormFile file, MediaType mediaType, BaseEntity e, Entype t)
    {
        var media = AddMedia(file, mediaType);

        var mediaRelation = new MediaRelation
        {
            EntityId = e.Id,
            EntityType = t,
            MediaId = media.Id,
            MediaType = media.MediaType,
            IsDefault = false,
        };

        _relationRepository.Add(mediaRelation);
        _relationRepository.SaveChange();
    }
   
    public void DeleteMedia(int id)
    {
        throw new NotImplementedException();
    }

    public void DeleteMediaByEntity(int entityId, Entype entityType)
    {
        throw new NotImplementedException();
    }

    public MediaResource UpdateResource(int id, IFormFile file)
    {
        throw new NotImplementedException();
    }
}