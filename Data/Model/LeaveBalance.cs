using SiGaHRMS.Data.Model.Entity;
using SiGaHRMS.Data.Model.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiGaHRMS.Data.Model;

public class LeaveBalance : FullAuditedEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long LeaveBalanceId { get; set; }

    [Required]
    public short Year { get; set; }

    public short EarnedLeaves { get; set; }

    public short CasualLeaves { get; set; }

    public short SickLeaves { get; set; }

    public short MaternityLeaves { get; set; }

    public short CompensatoryOffs { get; set; }

    public short MarriageLeaves { get; set; }

    public short PaternityLeaves { get; set; }

    public short BereavementLeaves { get; set; }

    public short LossofPayLeaves { get; set; }

    public short EarnedLeavesAvailaed { get; set; }

    public short CasualLeavesAvailaed { get; set; }

    public short SickLeavesAvailaed { get; set; }

    public short MaternityLeavesAvailaed { get; set; }

    public short CompensatoryOffsAvailaed { get; set; }

    public short MarriageLeavesAvailaed { get; set; }

    public short PaternityLeavesAvailaed { get; set; }

    public short BereavementLeavesAvailaed { get; set; }

    public short LossofPayLeavesAvailaed { get; set; }

    [Required]
    [MaxLength(20)]
    public LeaveBalanceStatus LeaveBalanceStatus { get; set; }

    [Required]
    public long EmployeeId { get; set; }

    public Employee Employee { get; set; }
}
