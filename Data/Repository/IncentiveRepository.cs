using SiGaHRMS.Data.DataContext;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;


namespace SiGaHRMS.Data.Repository;

public class IncentiveRepository : GenericRepository<Incentive>, IIncentiveRepository
{
    public IncentiveRepository(AppDbContext Context) : base(Context)
    {
    }

}
