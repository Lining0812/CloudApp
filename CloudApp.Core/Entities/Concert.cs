namespace CloudApp.Core.Entities
{
    /// <summary>
    /// 演出实体类
    /// </summary>
    public class Concert : Activity
    {
        /// <summary>
        /// 演唱会地址
        /// </summary>
        public required string Location { get; set; }
        /// <summary>
        /// 封面图片
        /// </summary>
        public string? CoverUrl { get; set; }
        /// <summary>
        /// 导航属性 - 演唱会歌单
        /// </summary>
        public Album? Album { get; set; }
        public int? AlbumId { get; set; }
    }
}
