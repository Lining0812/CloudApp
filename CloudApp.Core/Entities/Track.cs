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
        public string Title { get; set; }
        /// <summary>
        /// 副标题
        /// </summary>
        public string Subtitle { get; set; }
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
        /// 单曲链接URL
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// 封面图片URL
        /// </summary>
        public string CoverImageUrl { get; set; }
        /// <summary>
        /// 原唱
        /// </summary>
        public string Artist { get; set; }
        /// <summary>
        /// 作曲者
        /// </summary>
        public string Composer { get; set; }
        /// <summary>
        /// 作词者
        /// </summary>
        public string Lyricist { get; set; }

        /// <summary>
        /// 导航属性，所属专辑
        /// </summary>
        public Album? Album { get; set; }

        /// <summary>
        /// 外键字段，所属专辑Id
        /// </summary>
        public int? AlbumId { get; set; }
    }
}