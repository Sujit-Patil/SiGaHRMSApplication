using SiGaHRMS.Data.Model.Entity;
using SiGaHRMS.Data.Model.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiGaHRMS.Data.Model;

public class Employee : FullAuditedEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long EmployeeId { get; set; }

    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    public string Gender { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string ContactNumber { get; set; }

    public string? AltContactNumber { get; set; }

    public string? PersonalEmail { get; set; }

    public string CompanyEmail { get; set; } 

    public DateOnly DateOfJoining { get; set; }

    public string CurrentDesignation { get; set; }

    public decimal CurrentGrossSalary { get; set; }

    public DateOnly? DateOfRelieving { get; set; }

    public EmployeeStatus EmployeeStatus { get; set; }

    [ForeignKey("TeamLead")]
    public long? TeamLeadId { get; set; }

    public Employee? TeamLead { get; set; }

    [ForeignKey("ReportingManagerId")]
    public long? ReportingManagerId { get; set; }

    public Employee? ReportingManager { get; set; }
}

