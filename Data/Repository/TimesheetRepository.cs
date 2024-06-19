using SiGaHRMS.Data.DataContext;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;


namespace SiGaHRMS.Data.Repository;

public class TimesheetRepository : GenericRepository<Timesheet>, ITimesheetRepository
{
    public TimesheetRepository(AppDbContext Context) : base(Context)
    {
    }

}
