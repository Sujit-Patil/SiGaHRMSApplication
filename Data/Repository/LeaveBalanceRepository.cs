using SiGaHRMS.Data.DataContext;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;


namespace SiGaHRMS.Data.Repository;

public class LeaveBalanceRepository : GenericRepository<LeaveBalance>, ILeaveBalanceRepository
{
    public LeaveBalanceRepository(AppDbContext Context) : base(Context)
    {
    }

}
