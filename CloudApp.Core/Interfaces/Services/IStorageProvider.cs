using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace CloudApp.Core.Interfaces.Services
{
    public interface IStorageProvider
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

        string SaveFile(IFormFile file, string filepath);
        void DeleteFile(string filepath);
        void UpdateFile(string filepath);
    }
}
