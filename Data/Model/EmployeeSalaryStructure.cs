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
    public DateTime FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public decimal Basic { get; set; }

    public decimal HRA { get; set; }

    public decimal DA { get; set; }

    public decimal Conveyance { get; set; }

    public decimal MedicalAllowance { get; set; }

    public decimal SpecialAllowance { get; set; }

    public decimal TDS { get; set; }

    public long EmployeeId { get; set; }

    public Employee Employee { get; set; }
}
