using CloudApp.Core.Dtos;

namespace CloudApp.Core.Interfaces.Services
{
    public interface IConcertService
    {
        /// <summary>
        /// 创建演唱会
        /// </summary>
        /// <param name="concert"></param>
        void AddConcert(CreateConcertDto concert);
    }
}
