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

    public decimal Basic { get; set; }

    public decimal HRA { get; set; }

    public decimal DA { get; set; }

    public decimal Conveyance { get; set; }

    public decimal MedicalAllowance { get; set; }

    public decimal SpecialAllowance { get; set; }

    public decimal PT { get; set; }

    public decimal TDS { get; set; }

    public decimal LeaveDeduction { get; set; }

    public decimal OtherDeduction { get; set; }

    public int DaysInAMonth { get; set; }

    public int PresentDays { get; set; }

    public int Leaves { get; set; }

    public decimal GrossSalary { get; set; }

    public decimal NetSalary { get; set; }

    public long EmployeeId { get; set; }

    public Employee Employee { get; set; }
}
