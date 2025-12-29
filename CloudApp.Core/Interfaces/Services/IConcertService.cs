using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;

namespace CloudApp.Core.Interfaces.Services
{
    public interface IConcertService
    {
        /// <summary>
        /// 创建演唱会
        /// </summary>
        /// <param name="concert"></param>
        void AddConcert(CreateConcertDto concert);

        /// <summary>
        /// 删除演唱会
        /// </summary>
        /// <param name="id"></param>
        void DelectConcert(int id);

        /// <summary>
        /// 根据Id获取演唱会实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns>演唱会信息Dto</returns>
        public ConcertInfoDto? GetById(int id);

        /// <summary>
        /// 获取演唱会封面图片
        /// </summary>
        /// <param name="concertId">演唱会ID</param>
        /// <returns>图片流和内容类型</returns>
        (Stream stream, string contentType) GetCoverImage(Concert concert);
    }
}
