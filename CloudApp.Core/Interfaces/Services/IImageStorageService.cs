using Microsoft.AspNetCore.Http;

namespace CloudApp.Core.Interfaces.Services
{
    public interface IImageStorageService
    {
        string SaveAlbumImage(IFormFile image);

        string SaveConcertImage(IFormFile image);

        string SaveTrackImage(IFormFile image);

        /// <summary>
        /// 获取图片流。有问题，返回url即可，由前端渲染资源
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        (Stream stream, string contentType) GetImage(string imagePath);
    }
}
