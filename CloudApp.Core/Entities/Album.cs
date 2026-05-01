
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
        public required string Title { get; set; }
        /// <summary>
        /// 专辑描述
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 艺术家
        /// </summary>
        public required string Artist { get; set; }
        /// <summary>
        /// 发行日期
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// 导航属性 - 专辑中的单曲
        /// </summary>
        public ICollection<Track> Tracks { get; set; } = new List<Track>();
    }
}
