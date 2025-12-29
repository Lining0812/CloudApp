using Microsoft.AspNetCore.Http;

namespace CloudApp.Core.Interfaces.Services
{
    public interface IImageStorageService
    {
        /// <summary>
        /// 保存专辑封面
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        string SaveAlbumImage(IFormFile image);

        /// <summary>
        /// 保存演唱会封面
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        string SaveConcertImage(IFormFile image);

        /// <summary>
        /// 保存单曲封面
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        string SaveTrackImage(IFormFile image);

        string SaveImage(IFormFile image, string folder);

        /// <summary>
        /// 获取图片流。有问题，返回url即可，由前端渲染资源
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        (Stream stream, string contentType) GetImage(string imagePath);
    }
}
