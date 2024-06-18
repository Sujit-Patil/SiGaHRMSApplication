using SiGaHRMS.Data.Model.Entity;
using SiGaHRMS.Data.Model.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiGaHRMS.Data.Model;

public class Timesheet : FullAuditedEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long TimesheetId { get; set; }

    [Required]
    public DateTime TimesheetDate { get; set; }

    [MaxLength(20)]
    public TimeSheetStatus TimesheetStatus { get; set; }

    public long? Approver { get; set; }

    public Employee ApproverEmployee { get; set; }

    [Required]
    public long EmployeeId { get; set; }

    public Employee Employee { get; set; }
}
