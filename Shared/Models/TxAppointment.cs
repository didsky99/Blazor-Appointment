using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorAppointmentSystem.Shared.Models;

[Table("tx_appointment")]
public class TxAppointment
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(50)]
    public string Title { get; set; } = string.Empty;

    [StringLength(100)]
    public string? Description { get; set; }

    [StringLength(10)]
    public string? CustId { get; set; }

    [StringLength(50)]
    public string? CustName { get; set; }

    [StringLength(50)]
    public string? CustCompany { get; set; }

    [StringLength(4)]
    public string? Token { get; set; }

    public TimeSpan? Duration { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    [Required, StringLength(10)]
    public string AppointmentStatus { get; set; } = string.Empty;

    [Required, StringLength(10)]
    public string CreatedBy { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedAt { get; set; }
}
