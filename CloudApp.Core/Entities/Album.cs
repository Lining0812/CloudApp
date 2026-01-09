
namespace CloudApp.Core.Entities
{
    /// <summary>
    /// 专辑实体类
    /// </summary>
    public class Album : BaseEntity
    {
        /// <summary>
        /// 专辑名称
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// 专辑描述
        /// </summary>
        public string? Description { get; set; } = string.Empty;
        /// <summary>
        /// 艺术家
        /// </summary>
        public string Artist { get; set; } = string.Empty;
        /// <summary>
        /// 发行日期
        /// </summary>
        public DateTime ReleaseDate { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// 封面图片URL
        /// </summary>
        public string CoverImageUrl { get; set; } = string.Empty;
        /// <summary>
        /// 导航属性 - 专辑中的单曲列表
        /// </summary>
        public ICollection<Track> Tracks { get; set; } = new List<Track>();
    }
}
