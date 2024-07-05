using Microsoft.AspNetCore.Identity;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(IdentityUser IdentityUser, IEnumerable<string> roles, Employee? employee);
}
