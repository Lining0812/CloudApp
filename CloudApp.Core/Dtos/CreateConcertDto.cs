namespace CloudApp.Core.Dtos
{
    public class CreateConcertDto
    {
        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public string Location { get; set; }
    }
}
