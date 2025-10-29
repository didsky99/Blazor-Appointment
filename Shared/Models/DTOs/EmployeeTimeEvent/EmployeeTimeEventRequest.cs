namespace Models.Dtos.EmployeeTimeEvent;

public class EmployeeTimeEventRequest
{
   public string Nrp { get; set; } = null!;

    public DateTime EventDate { get; set; }

    public TimeSpan? ClockIn { get; set; }

    public string? LatLongIn { get; set; }

    public DateTimeOffset? TimeStampIn { get; set; }

    public TimeSpan? ClockOut { get; set; }

    public string? LatLongOut { get; set; }

    public DateTimeOffset? TimeStampOut { get; set; }

    public string? TimeZone { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }
}
