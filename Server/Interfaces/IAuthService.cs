using SiGaHRMS.Data.Entities.Api;
using SiGaHRMS.Data.Model.Dto;

namespace SiGaHRMS.ApiService.Interfaces;

public interface IAuthService
{
    Task<string> Register(RegistrationRequest registrationRequestDto);
    Task<LoginResponseDto> Login(LoginRequest loginRequestDto);
    Task<bool> AssignRole(string email, string roleName);
    Task<bool> CreateRole(string roleName);
}
