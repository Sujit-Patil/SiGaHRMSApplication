using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.ApiService.Service;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Repository;

namespace SiGaHRMS.HRMS.ApiService;

public static class ServiceCollectionExtenstion
{
    public static void AddService(IServiceCollection Services)
    {
       Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
       Services.AddScoped<IAuthService, AuthService>();
       Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
       Services.AddScoped<IEmployeeService, EmployeeService>();
       Services.AddScoped<IClientRepository, ClientRepository>();
       Services.AddScoped<IClientService, ClientService>();
       Services.AddScoped<IImageService, ImageService>();

    }
}
