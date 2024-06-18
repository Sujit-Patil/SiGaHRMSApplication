using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.ApiService.Service;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Repository;

namespace SiGaHRMS.HRMS.ApiService;

public static class ServiceCollectionExtenstion
{
    public static void AddService(IServiceCollection Services)
    {
        Services.AddTransient<IUserRepository, UserRepository>();
        Services.AddTransient<IUserService, UserService>();
    }
}
