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
        public required string Title { get; set; }
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
        public DateTime ReleaseDate { get; set; }
        /// <summary>
        /// 原唱
        /// </summary>
        public required string Artist { get; set; }
        /// <summary>
        /// 作曲者
        /// </summary>
        public required string Composer { get; set; }
        /// <summary>
        /// 作词者
        /// </summary>
        public required string Lyricist { get; set; }
        /// <summary>
        /// 封面图片
        /// </summary>
        public string? CoverUrl { get; set; }
        /// <summary>
        /// 单曲类型
        /// </summary>
        public TrackType Type { get; set; }

        /// <summary>
        /// 导航属性 - 专辑
        /// </summary>
        public Album? Album { get; set; }
        public int? AlbumId { get; set; }
    }
}