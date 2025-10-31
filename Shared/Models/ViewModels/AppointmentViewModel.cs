namespace BlazorAppointmentSystem.Shared.Models
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string CustId { get; set; } = string.Empty;
        public string CustName { get; set; } = string.Empty;
        public string? CustCompany { get; set; }
        public string Token { get; set; } = string.Empty;
        public TimeSpan? Duration { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string AppointmentStatus { get; set; } = string.Empty;
        public string StatusName { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
