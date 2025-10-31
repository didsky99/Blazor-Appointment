using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorAppointmentSystem.Shared.Models;

[Table("tb_customer")]
public class TbCustomer
{
    [Key, StringLength(10)]
    public string Id { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [StringLength(10)]
    public string? CompanyId { get; set; }

    public byte GenderId { get; set; }

    [StringLength(20)]
    public string? GenderText { get; set; }

    [Required, StringLength(10)]
    public string CreatedBy { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedAt { get; set; }
}
