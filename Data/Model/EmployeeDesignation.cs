using SiGaHRMS.Data.Model.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiGaHRMS.Data.Model;

public class EmployeeDesignation : FullAuditedEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long EmployeeDesignationId { get; set; }

    [Required]
    public DateTime FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public long EmployeeId { get; set; }

    public Employee Employee { get; set; }

    public int DesignationId { get; set; }

    public Designation Designation { get; set; }
}
