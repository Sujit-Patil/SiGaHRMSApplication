using SiGaHRMS.Data.DataContext;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.Data.Repository;

public class BillingPlatformRepository : GenericRepository<BillingPlatform>, IBillingPlatformRepository
{
    public BillingPlatformRepository(AppDbContext Context) : base(Context)
    {
    }

}
