using SiGaHRMS.Data.Model.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiGaHRMS.Data.Model;

public class EmployeeSalaryStructure : FullAuditedEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long EmployeeSalaryStructureId { get; set; }

    [Required]
    public DateOnly FromDate { get; set; }

    public DateOnly? ToDate { get; set; } = null;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Basic { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal HRA { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal DA { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Conveyance { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal MedicalAllowance { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal SpecialAllowance { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal TDS { get; set; }

    public long EmployeeId { get; set; }

    public Employee? Employee { get; set; }
}
