using Microsoft.AspNetCore.Identity;
using SiGaHRMS.Data.Model.AuthModel;

namespace SiGaHRMS.ApiService.Interfaces;

public interface IAuthService
{
    Task<IdentityResult> Register(RegisterModel registerModel);
    Task<string> Login(LoginModel loginModel);
    Task<IdentityResult> AssignRole(AssignRoleModel assignRoleModel);
    Task<IdentityResult> CreateRole(CreateRoleModel createRoleModel);
}
