using SiGaHRMS.Data.DataContext;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;


namespace SiGaHRMS.Data.Repository;

public class TaskNameRepository : GenericRepository<TaskName>, ITaskNameRepository
{
    public TaskNameRepository(AppDbContext Context) : base(Context)
    {
    }

}
