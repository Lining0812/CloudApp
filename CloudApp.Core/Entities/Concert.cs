namespace CloudApp.Core.Entities
{
    /// <summary>
    /// 演出实体类
    /// </summary>
    public class Concert : BaseEntity
    {
        /// <summary>
        /// 演唱会标题
        /// </summary>
        public required string Title { get; set; }
        /// <summary>
        /// 演出介绍
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartAt { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndAt { get; set; }
        /// <summary>
        /// 演出地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 封面图片
        /// </summary>
        public string CoverImageUrl { get; set; }

        /// <summary>
        /// 导航属性 - 演唱会歌单
        /// </summary>
        public Album? Album { get; set; }
        public int? AlbumId { get; set; }
        /// <summary>
        /// 导航属性 - 资源
        /// </summary>
        //public ICollection<MediaRelation> MediaRelations { get; set; } = new List<MediaRelation>();
    }
}
