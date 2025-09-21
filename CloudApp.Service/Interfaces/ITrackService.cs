using CloudApp.Core.Dtos;
using CloudApp.Core.Entities;
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
        ICollection<TrackInfoDto> GetAllTracks();
    }
}
