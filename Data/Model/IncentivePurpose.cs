using SiGaHRMS.Data.Model.Entity;
using System.ComponentModel.DataAnnotations;

namespace SiGaHRMS.Data.Model;

public class IncentivePurpose : FullAuditedEntity
{
    [Key]
    public int IncentivePurposeId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Purpose { get; set; }
}
