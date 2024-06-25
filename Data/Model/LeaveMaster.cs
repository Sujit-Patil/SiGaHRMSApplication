using SiGaHRMS.Data.Model.Entity;
using System.ComponentModel.DataAnnotations;

namespace SiGaHRMS.Data.Model;

public class LeaveMaster : FullAuditedEntity
{
    [Key]
    public int LeaveMasterId { get; set; }

    [Required]
    public string LeaveType { get; set; }

    [Required]
    public int LeaveCount { get; set; }
}
