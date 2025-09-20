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
        [Key]
        public int Id { get; set; }
        [JsonPropertyName("歌名")]
        public string Title { get; set; } = string.Empty;
        public string? Subtitle { get; set; }
        [JsonPropertyName("描述")]
        public string? Description { get; set; }
        [JsonPropertyName("所属专辑")]
        public string Albumtitle { get; set; }
        [JsonPropertyName("作曲")]
        public string? Composer { get; set; }
        [JsonPropertyName("作词")]
        public string? Lyricist { get; set; }

    }
}
