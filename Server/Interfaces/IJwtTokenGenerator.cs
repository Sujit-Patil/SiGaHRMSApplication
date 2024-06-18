using SiGaHRMS.Data.Model.AuthModel;

namespace SiGaHRMS.ApiService.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
}
