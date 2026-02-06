using CloudApp.Core.Enums;

namespace CloudApp.Core.Entities
{
    /// <summary>
    /// 单曲实体类
    /// </summary>
    public class Track : BaseEntity
    {
        /// <summary>
        /// 单曲标题
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// 副标题
        /// </summary>
        public string? Subtitle { get; set; }
        /// <summary>
        /// 单曲描述
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 时长
        /// </summary>
        public TimeSpan Duration { get; set; }
        /// <summary>
        /// 发行日期
        /// </summary>
        public DateTime ReleaseDate { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// 原唱
        /// </summary>
        public string Artist { get; set; } = string.Empty;
        /// <summary>
        /// 作曲者
        /// </summary>
        public string Composer { get; set; } = string.Empty;
        /// <summary>
        /// 作词者
        /// </summary>
        public string Lyricist { get; set; } = string.Empty;
        /// <summary>
        /// 封面图片
        /// </summary>
        public string CoverImage { get; set; } = string.Empty;
        /// <summary>
        /// 单曲类型
        /// </summary>
        public TrackType Type { get; set; } = TrackType.Single;
        /// <summary>
        /// 专辑导航
        /// </summary>
        public Album? Album { get; set; } 
        public int? AlbumId { get; set; }

        /// <summary>
        /// 演唱会导航
        /// </summary>
        public Concert? Concert { get; set; }
        public int? ConcertId { get; set; }
    }
}