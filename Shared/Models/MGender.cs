using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorAppointmentSystem.Shared.Models;

[Table("m_gender")]
public class MGender
{
    [Key]
    public byte Id { get; set; }

    [Required, StringLength(20)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(10)]
    public string CreatedBy { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedAt { get; set; }
}
