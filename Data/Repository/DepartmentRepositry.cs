using SiGaHRMS.Data.DataContext;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.Data.Repository;

public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
{
    public DepartmentRepository(AppDbContext Context) : base(Context)
    {
    }

}
