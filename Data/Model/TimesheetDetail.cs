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

    public int TaskId { get; set; }

    public TaskName? Task { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal HoursSpent { get; set; }

    public bool IsBillable { get; set; }

    public TaskType TaskType { get; set; }

    public long? TimesheetId { get; set; }

    public Timesheet? Timesheet { get; set; }

    [NotMapped]
    public DateOnly? TimeSheetDate { get; set; }
}
