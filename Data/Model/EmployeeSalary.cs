using SiGaHRMS.Data.Model.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiGaHRMS.Data.Model;

public class EmployeeSalary : FullAuditedEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long EmployeeSalaryId { get; set; }

    [Required]
    public DateTime SalaryForAMonth { get; set; }

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
    public decimal PT { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal TDS { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal LeaveDeduction { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal OtherDeduction { get; set; }

    public int DaysInAMonth { get; set; }

    public int PresentDays { get; set; }

    public int Leaves { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal GrossSalary { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal NetSalary { get; set; }

    public long EmployeeId { get; set; }

    public Employee? Employee { get; set; }
}
