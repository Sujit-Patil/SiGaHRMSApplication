using Microsoft.AspNetCore.Identity;

namespace SiGaHRMS.ApiService.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(IdentityUser IdentityUser, IEnumerable<string> roles);
}
