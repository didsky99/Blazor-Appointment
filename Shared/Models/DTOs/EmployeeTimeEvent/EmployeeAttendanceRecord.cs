
namespace Models.Dtos.EmployeeTimeEvent;

public class EmployeeAttendanceRecord
{
    public string Nrp { get; set; } = null!;

    public DateTime EventDate { get; set; }

    public TimeSpan? ClockIn { get; set; }

    public TimeSpan? ClockOut { get; set; }

    public string WorkingModel { get; set; } = null!;
    
    public string Result { get; set; } = null!;

    public int Mood { get; set; } = 5;

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }
}
