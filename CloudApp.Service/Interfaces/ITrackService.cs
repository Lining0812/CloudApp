using CloudApp.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudApp.Service.Interfaces
{
    public interface ITrackService
    {
        void AddTrack(CreateTrackDto model);
        IEnumerable<TrackInfoDto> GetAllTracks();
    }
}
