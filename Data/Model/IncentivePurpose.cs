using SiGaHRMS.Data.Model.Entity;
using System.ComponentModel.DataAnnotations;

namespace SiGaHRMS.Data.Model;

public class IncentivePurpose : FullAuditedEntity
{
    [Key]
    public int IncentivePurposeId { get; set; }

    [MaxLength(100)]
    public required string Purpose { get; set; }
}
