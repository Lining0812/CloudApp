using CloudApp.Core.Dtos.Concert;

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
        /// 更新演唱会
        /// </summary>
        /// <param name="id"></param>
        void UpdateConcert(int id, CreateConcertDto model);

        /// <summary>
        /// 根据Id获取演唱会实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns>演唱会信息Dto</returns>
        public ConcertInfoDto? GetById(int id);

    }
}
