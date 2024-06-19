using SiGaHRMS.Data.DataContext;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;


namespace SiGaHRMS.Data.Repository;

public class IncentivePurposeRepository : GenericRepository<IncentivePurpose>, IIncentivePurposeRepository
{
    public IncentivePurposeRepository(AppDbContext Context) : base(Context)
    {
    }

}
