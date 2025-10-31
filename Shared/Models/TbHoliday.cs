using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorAppointmentSystem.Shared.Models;

[Table("tb_holidays")]
public class TbHolidays
{
    [Key]
    public byte Id { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required, StringLength(20)]
    public string HolidayName { get; set; } = string.Empty;

    [StringLength(50)]
    public string? Description { get; set; }

    public byte HolidayTypeId { get; set; }

    [Required, StringLength(10)]
    public string CreatedBy { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedAt { get; set; }
}
