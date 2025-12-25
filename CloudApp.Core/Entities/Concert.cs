namespace CloudApp.Core.Entities
{
    /// <summary>
    /// 演出实体类
    /// </summary>
    public class Concert : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string Location { get; set; }

        public string? CoverImageUrl { get; set; }
    }
}
