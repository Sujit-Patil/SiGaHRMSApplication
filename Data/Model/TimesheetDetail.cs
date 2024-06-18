using SiGaHRMS.Data.Model.Entity;
using SiGaHRMS.Data.Model.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiGaHRMS.Data.Model;

public class TimeSheetDetail : FullAuditedEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long TimeSheetDetailId { get; set; }

    [Required]
    [MaxLength(2000)]
    public string Task { get; set; }

    public short HoursSpent { get; set; }

    public bool IsBillable { get; set; }

    public TaskType TaskType { get; set; }

    public int? ProjectId { get; set; }

    public Project Project { get; set; }

    public long TimesheetId { get; set; }
    public Timesheet Timesheet { get; set; }

    public int? ClientId { get; set; }
    public Client Client { get; set; }
}
