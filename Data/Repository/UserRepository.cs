using SiGaHRMS.Data.DataContext;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;


namespace SiGaHRMS.Data.Repository;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext Context) : base(Context)
    {
    }

}
