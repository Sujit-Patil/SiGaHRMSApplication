
namespace SiGaHRMS.Data.Model.Dto;

public class LeaveRequestDto
{
    public long? EmployeeId { get; set; } = null;

    public DateOnly? FormDate { get; set; }

    public DateOnly? ToDate { get; set; }
}
