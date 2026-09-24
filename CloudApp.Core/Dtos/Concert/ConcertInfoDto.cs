namespace CloudApp.Core.Dtos.Concert
{
    public class ConcertInfoDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Location { get; set; }
        public string? CoverUrl { get; set; }
        public IEnumerable<string> Tracks { get; set; }
    }
}
