using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorAppointmentSystem.Shared.Models;

[Table("m_settings")]
public class MSettings
{
    [Key]
    public byte Id { get; set; }

    [StringLength(20)]
    public string? SettingName { get; set; }

    [StringLength(50)]
    public string? Value { get; set; }

    [Required, StringLength(10)]
    public string CreatedBy { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedAt { get; set; }
}
