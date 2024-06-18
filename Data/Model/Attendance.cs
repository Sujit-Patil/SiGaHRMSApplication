using SiGaHRMS.Data.Model.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiGaHRMS.Data.Model;

public class Attendance : FullAuditedEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long AttendanceId { get; set; }

    [Required]
    public DateTime AttendanceDate { get; set; }

    [Required]
    public DateTime InTime { get; set; }

    public DateTime? OutTime { get; set; }

    public long EmployeeId { get; set; }

    public Employee Employee { get; set; }
}
