
namespace SiGaHRMS.Data.Model.Dto;

public class ProjectBillingResponse
{
    public Dictionary<DateOnly, decimal> WeeklyBilling { get; set; }
    public Dictionary<int, decimal> MonthlyBilling { get; set; }
}