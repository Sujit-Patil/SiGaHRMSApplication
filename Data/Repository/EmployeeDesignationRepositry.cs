using SiGaHRMS.Data.DataContext;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;


namespace SiGaHRMS.Data.Repository;

public class EmployeeDesignationRepository : GenericRepository<EmployeeDesignation>, IEmployeeDesignationRepository
{
    public EmployeeDesignationRepository(AppDbContext Context) : base(Context)
    {
    }

}
