using SiGaHRMS.Data.Model.Entity;
using System.ComponentModel.DataAnnotations;

namespace SiGaHRMS.Data.Model;

public class Department : FullAuditedEntity
{
    [Key]
    public int DepartmentId { get; set; }

    [Required]
    [MaxLength(100)]
    public string DepartmentName { get; set; }
}
