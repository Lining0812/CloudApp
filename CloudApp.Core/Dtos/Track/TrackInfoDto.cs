using CloudApp.Core.Enums;

namespace CloudApp.Core.Dtos.Track
{
    /// <summary>
    /// 单曲信息（返回给前端）
    /// </summary>
    public class TrackInfoDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Subtitle { get; set; }
        public string? Description { get; set; }
        /// <summary>
        /// 时长（ISO 8601 时长格式，如 PT3M30S）
        /// </summary>
        public TimeSpan Duration { get; set; }
        /// <summary>
        /// 时长（秒），前端格式化 mm:ss 直接用这个字段，避免解析 ISO 时长
        /// </summary>
        public int DurationSeconds => (int)Duration.TotalSeconds;
        public DateTime ReleaseDate { get; set; }
        public string Artist { get; set; } = string.Empty;
        public string Composer { get; set; } = string.Empty;
        public string Lyricist { get; set; } = string.Empty;
        /// <summary>
        /// 封面图（与前端 coverUrl 字段对齐；可能是 https 链接或云存储 fileID）
        /// </summary>
        public string? CoverUrl { get; set; }
        /// <summary>
        /// 跳转链接，为空表示本曲不可跳转
        /// </summary>
        public string? LinkUrl { get; set; }
        public TrackType Type { get; set; }
        public int? AlbumId { get; set; }
        public string? AlbumTitle { get; set; }
    }
}
