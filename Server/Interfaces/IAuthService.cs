using SiGaHRMS.Data.Model.AuthModel;
using SiGaHRMS.Data.Model.Dto;

namespace SiGaHRMS.ApiService.Interfaces;

public interface IAuthService
{
    Task<string> Register(RegisterModel registrationRequestDto);
    Task<string> Login(string email, string password);
    Task<bool> AssignRole(string email, string roleName);
    Task<bool> CreateRole(string roleName);
}
