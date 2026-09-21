namespace CloudApp.Core.Dtos.CheckIn
{
    public class CheckInResult
    {
        public bool Success { get; set; }
        public bool AlreadyCheckIn { get; set; }
        public int Streak { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
