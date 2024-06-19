using SiGaHRMS.Data.DataContext;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;


namespace SiGaHRMS.Data.Repository;

public class ClientRepository : GenericRepository<Client>, IClientRepository
{
    public ClientRepository(AppDbContext Context) : base(Context)
    {
    }

}
