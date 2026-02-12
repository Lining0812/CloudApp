using CloudApp.Core.Entities;
using CloudApp.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace CloudApp.Core.Interfaces.Services;

public interface IMediaService
{
    /// <summary>
    /// 增加媒体资源
    /// </summary>
    /// <param name="file"></param>
    /// <param name="mediaType"></param>
    MediaResource AddMedia(IFormFile file, MediaType mediaType);

    /// <summary>
    /// 增加媒体资源并增加实体关系
    /// </summary>
    /// <param name="e"></param>
    void AddMediaWithRelation(IFormFile file, MediaType mediaType, BaseEntity e, Entype t);

    void DeleteMedia(int id);

    /// <summary>
    /// 删除实体关联的所有媒体资源
    /// </summary>
    /// <param name="entityId"></param>
    /// <param name="entityType"></param>
    void DeleteMediaByEntity(int entityId, Entype entityType);

    MediaResource UpdateResource(int id, IFormFile file);
}