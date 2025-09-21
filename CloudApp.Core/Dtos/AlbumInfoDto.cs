using CloudApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CloudApp.Core.Dtos
{
    public class AlbumInfoDto
    {
        [JsonPropertyName("专辑名")]
        public string Title { get; set; }

        [JsonPropertyName("专辑描述")]
        public string Description { get; set; }

        [JsonPropertyName("演唱者")]
        public string Artist { get; set; }

        [JsonPropertyName("单曲列表")]
        public List<string> Tracks { get; set; }

        public AlbumInfoDto()
        {
        }

        public AlbumInfoDto(Album album)
        {
            Title = album.Title;
            Description = album.Description;
            Artist = album.Artist;
            Tracks = album.Tracks.Select(t => t.Title).ToList();
        }

    }
}
