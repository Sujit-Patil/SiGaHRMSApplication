

namespace SiGaHRMS.Data.Model.Dto;

public class RequestDto
{
    public long? EmployeeId { get; set; }

    public DateOnly? FormDate { get; set; }

    public DateOnly? ToDate { get; set; }
}
