using Microsoft.AspNetCore.Identity;
using SiGaHRMS.Data.Entities.Api;

namespace SiGaHRMS.ApiService.Interfaces;

// <summary>
/// Interface for handling user authentication and authorization.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Registers a new user asynchronously.
    /// </summary>
    /// <param name="registrationRequest">The registration request data.</param>
    /// <returns>An <see cref="IdentityResult"/> representing the outcome of the registration operation.</returns>
    Task<IdentityResult> RegisterUserAsync(RegistrationRequest registrationRequest);

    /// <summary>
    /// Logs in a user asynchronously.
    /// </summary>
    /// <param name="loginRequest">The login request data.</param>
    /// <returns>A JWT token string upon successful login.</returns>
    Task<string> LoginUserAsync(LoginRequest loginRequest);

    /// <summary>
    /// Assigns a role to a user asynchronously.
    /// </summary>
    /// <param name="assignRoleRequest">The request to assign a role to a user.</param>
    /// <returns>An <see cref="IdentityResult"/> representing the outcome of the role assignment operation.</returns>
    Task<IdentityResult> AssignRoleToUserAsync(AssignRoleRequest assignRoleRequest);

    /// <summary>
    /// Creates a new user role asynchronously.
    /// </summary>
    /// <param name="roleToCreate">The name of the role to create.</param>
    /// <returns>An <see cref="IdentityResult"/> representing the outcome of the role creation operation.</returns>
    Task<IdentityResult> CreateUserRoleAsync(string roleToCreate);
}
