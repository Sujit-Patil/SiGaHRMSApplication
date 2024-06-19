using Microsoft.AspNetCore.Identity;
using SiGaHRMS.Data.Model.AuthModel;

namespace SiGaHRMS.ApiService.Interfaces;

public interface IAuthService
{
    Task<IdentityResult> RegisterUserAsync(RegisterModel registerModel);
    Task<string> LoginUserAsync(LoginModel loginModel);
    Task<IdentityResult> AssignRoleToUserAsync(AssignRoleModel assignRoleModel);
    Task<IdentityResult> CreateUserRoleAsync(CreateRoleModel createRoleModel);
}
