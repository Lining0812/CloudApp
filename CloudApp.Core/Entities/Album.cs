
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
        public string Title { get; set; }
        /// <summary>
        /// 专辑描述
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 艺术家
        /// </summary>
        public string Artist { get; set; }
        /// <summary>
        /// 发行日期
        /// </summary>
        public DateTime ReleaseDate { get; set; }
        /// <summary>
        /// 导航属性 - 专辑中的单曲列表
        /// </summary>
        public IEnumerable<Track> Tracks { get; set; } = new List<Track>();
    }
}
