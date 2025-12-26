using CloudApp.Core.Dtos;

namespace CloudApp.Core.Interfaces
{
    public interface ITrackService
    {
        #region ͬ同步方法
        /// <summary>
        /// 添加单曲
        /// </summary>
        /// <param name="model"></param>
        void AddTrack(CreateTrackDto model);
        /// <summary>
        /// 获取所有单曲
        /// </summary>
        /// <returns></returns>
        ICollection<TrackInfoDto> GetAllTracks();
        /// <summary>
        /// 根据Id获取专辑信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TrackInfoDto GetTrackById(int id);
        /// <summary>
        /// 根据Id更新专辑信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        void UpdateTrack(int id, CreateTrackDto model);
        /// <summary>
        /// 根据Id删除单曲
        /// </summary>
        /// <param name="id"></param>
        void DeleteTrack(int id);
        #endregion

        #region �첽������������
        /// <summary>
        /// �첽���ӹ��
        /// </summary>
        /// <param name="model"></param>
        //Task AddTrackAsync(CreateTrackDto model);

        /// <summary>
        /// �첽��ȡ���й��
        /// </summary>
        /// <returns></returns>
        //Task<ICollection<TrackInfoDto>> GetAllTracksAsync();

        /// <summary>
        /// �첽����ID��ȡ�����Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //Task<TrackInfoDto> GetTrackByIdAsync(int id);

        /// <summary>
        /// �첽���¹����Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        //Task UpdateTrackAsync(int id, CreateTrackDto model);

        /// <summary>
        /// �첽ɾ�����
        /// </summary>
        /// <param name="id"></param>
        //Task DeleteTrackAsync(int id);
        #endregion

        #region ��չ���ܣ�������
        /// <summary>
        /// �첽����ר��ID��ȡ���
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        //Task<ICollection<TrackInfoDto>> GetTracksByAlbumIdAsync(int albumId);

        /// <summary>
        /// ��ҳ��ȡ���
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        //ICollection<TrackInfoDto> GetTracksWithPagination(int pageNumber, int pageSize);

        /// <summary>
        /// �첽��ҳ��ȡ���
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        //Task<ICollection<TrackInfoDto>> GetTracksWithPaginationAsync(int pageNumber, int pageSize);
        #endregion
    }
}
