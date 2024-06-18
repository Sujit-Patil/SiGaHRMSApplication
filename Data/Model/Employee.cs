using SiGaHRMS.Data.Model.Entity;
using SiGaHRMS.Data.Model.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiGaHRMS.Data.Model;

public class Employee : FullAuditedEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long EmployeeId { get; set; }

    [MaxLength(20)]
    public EmployeeStatus EmployeeStatus { get; set; }

    [Required]
    [ForeignKey("UserId")]
    public long UserId { get; set; }

    public User User { get; set; }

    [ForeignKey("TeamLead")]
    public long? TeamLeadId { get; set; }

    public Employee TeamLead { get; set; }

    [ForeignKey("ReportingManagerId")]
    public long? ReportingManagerId { get; set; }

    public Employee ReportingManager { get; set; }
}

