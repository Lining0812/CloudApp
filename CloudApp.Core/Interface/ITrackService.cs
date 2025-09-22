using CloudApp.Core.Dtos;

namespace CloudApp.Core.Interface
{
    public interface ITrackService
    {
        void AddTrack(CreateTrackDto model);
        ICollection<TrackInfoDto> GetAllTracks();
    }
}
