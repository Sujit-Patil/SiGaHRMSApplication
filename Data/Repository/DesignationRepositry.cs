using SiGaHRMS.Data.DataContext;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;


namespace SiGaHRMS.Data.Repository;

public class DesignationRepository : GenericRepository<Designation>, IDesignationRepository
{
    public DesignationRepository(AppDbContext Context) : base(Context)
    {
    }

}
