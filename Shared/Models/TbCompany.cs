using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorAppointmentSystem.Shared.Models;

[Table("tb_company")]
public class TbCompany
{
    [Key, StringLength(10)]
    public string Id { get; set; } = string.Empty;

    [StringLength(50)]
    public string? Name { get; set; }

    [Required, StringLength(10)]
    public string CreatedBy { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedAt { get; set; }
}
