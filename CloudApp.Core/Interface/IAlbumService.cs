using CloudApp.Core.Dtos;

namespace CloudApp.Core.Interface
{
    public interface IAlbumService
    {
        /// <summary>
        /// ����ר��
        /// </summary>
        /// <param name="album"></param>
        int AddAlbum(CreateAlbumDto album);
        
        /// <summary>
        /// ��ȡ����ר����Ϣ
        /// </summary>
        /// <returns></returns>
        ICollection<AlbumInfoDto> GetAllAlbums();
        /// <summary>
        /// ����ID��ȡר����Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AlbumInfoDto GetAlbumById(int id);
        /// <summary>
        /// ����ID���Ҳ�����ר����Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="album"></param>
        void UpdateAlbum(int id, CreateAlbumDto album);
        /// <summary>
        /// ����IDɾ��ר��
        /// </summary>
        /// <param name="id"></param>
        void DeleteAlbum(int id);
    }
}
