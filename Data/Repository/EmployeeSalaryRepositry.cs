using SiGaHRMS.Data.DataContext;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;


namespace SiGaHRMS.Data.Repository;

public class EmployeeSalaryRepository : GenericRepository<EmployeeSalary>, IEmployeeSalaryRepository
{
    public EmployeeSalaryRepository(AppDbContext Context) : base(Context)
    {
    }

}
