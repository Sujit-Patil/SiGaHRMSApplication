using SiGaHRMS.Data.Model.Entity;
using SiGaHRMS.Data.Model.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiGaHRMS.Data.Model;

public class Project : FullAuditedEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProjectId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Title { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal RateUSD { get; set; }

    public decimal RateINR { get; set; }

    public int WeeklyLimit { get; set; }

    [MaxLength(50)]
    public BillingType BillingType { get; set; }

    public string Status { get; set; }

    public int ClientId { get; set; }

    public Client Client { get; set; }

    public int BillingPlatformId { get; set; }

    public BillingPlatform BillingPlatform { get; set; }
}
