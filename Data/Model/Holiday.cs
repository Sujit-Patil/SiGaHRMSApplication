using SiGaHRMS.Data.Model.Entity;
using System.ComponentModel.DataAnnotations;

namespace SiGaHRMS.Data.Model;

public class Holiday : FullAuditedEntity
{
    [Key]
    public int HolidayId { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [MaxLength(100)]
    public string Description { get; set; }
}
