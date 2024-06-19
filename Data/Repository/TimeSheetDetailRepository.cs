using SiGaHRMS.Data.DataContext;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;


namespace SiGaHRMS.Data.Repository;

public class TimeSheetDetailRepository : GenericRepository<TimeSheetDetail>, ITimeSheetDetailRepository
{
    public TimeSheetDetailRepository(AppDbContext Context) : base(Context)
    {
    }

}
