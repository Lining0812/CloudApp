
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
        /// 导航属性 - 专辑中的单曲
        /// </summary>
        public ICollection<Track> Tracks { get; set; } = new List<Track>();
        /// <summary>
        /// 导航属性 - 资源
        /// </summary>
        public ICollection<MediaRelation> MediaRelations { get; set; } = new List<MediaRelation>();
    }
}
