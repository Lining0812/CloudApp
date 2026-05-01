namespace CloudApp.Core.Entities
{
    /// <summary>
    /// 行程（待完善）
    /// </summary>
    public class Itinerary : BaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }

        public string EventType { get; set; }
        public DateTime StartTime { get; set; }
    }
}
