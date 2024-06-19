using Microsoft.AspNetCore.Identity;
using SiGaHRMS.Data.Entities.Api;
using SiGaHRMS.Data.Model.AuthModel;

namespace SiGaHRMS.ApiService.Interfaces;

public interface IAuthService
{
    Task<IdentityResult> RegisterUserAsync(RegistrationRequest registrationRequest);
    Task<string> LoginUserAsync(LoginRequest loginRequest);
    Task<IdentityResult> AssignRoleToUserAsync(AssignRoleRequest assignRoleRequest);
    Task<IdentityResult> CreateUserRoleAsync(string roleToCreate);
}
