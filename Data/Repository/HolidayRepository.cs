using SiGaHRMS.Data.DataContext;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;


namespace SiGaHRMS.Data.Repository;

public class HolidayRepository : GenericRepository<Holiday>, IHolidayRepository
{
    public HolidayRepository(AppDbContext Context) : base(Context)
    {
    }

}
