using CloudApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CloudApp.Core.Dtos
{
    public class TrackInfoDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Composer { get; set; }
        public string Lyricist { get; set; }

        private TrackInfoDto()
        {
        }

        public TrackInfoDto(Track track)
        {
            this.Title = track.Title + track.Subtitle;
            this.Description = track.Description;
            this.Composer = track.Composer;
            this.Lyricist = track.Lyricist;
        }
    }
}
