using Microsoft.AspNetCore.Identity;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Interfaces;

/// <summary>
/// Interface for generating JWT tokens.
/// </summary>
public interface IJwtTokenGenerator
{
    /// <summary>
    /// Generates a JWT token for the specified IdentityUser with roles and optional employee information.
    /// </summary>
    /// <param name="identityUser">The IdentityUser for whom the token is generated.</param>
    /// <param name="roles">The roles assigned to the user.</param>
    /// <param name="employee">Optional employee information associated with the user.</param>
    /// <returns>The generated JWT token as a string.</returns>
    string GenerateToken(IdentityUser identityUser, IEnumerable<string> roles, Employee? employee);
}

