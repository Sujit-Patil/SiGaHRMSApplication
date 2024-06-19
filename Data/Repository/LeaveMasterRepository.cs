using SiGaHRMS.Data.DataContext;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;


namespace SiGaHRMS.Data.Repository;

public class LeaveMasterRepository : GenericRepository<LeaveMaster>, ILeaveMasterRepository
{
    public LeaveMasterRepository(AppDbContext Context) : base(Context)
    {
    }

}
