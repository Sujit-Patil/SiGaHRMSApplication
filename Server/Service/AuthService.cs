using Microsoft.AspNetCore.Identity;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Entities.Api;
using SiGaHRMS.Data.Model;


namespace SiGaHRMS.ApiService.Service;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IEmployeeService _employeeService;

    /// <summary>
    /// Initializes a new instance of the AuthService class.
    /// </summary>
    /// <param name="jwtTokenGenerator">The service responsible for generating JWT tokens.</param>
    /// <param name="userManager">The UserManager for managing user data.</param>
    /// <param name="roleManager">The RoleManager for managing roles.</param>
    /// <param name="employeeService">The service for managing employee-related operations.</param>
    public AuthService(
        IJwtTokenGenerator jwtTokenGenerator,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IEmployeeService employeeService)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userManager = userManager;
        _roleManager = roleManager;
        _employeeService = employeeService;
    }

    /// <inheritdoc/>
    public async Task<IdentityResult> CreateUserRoleAsync(string roleToCreate)
    {
        IdentityResult result = new();

        if (!await _roleManager.RoleExistsAsync(roleToCreate))
        {
            result = await _roleManager.CreateAsync(new IdentityRole(roleToCreate));
            return result;
        }
        return result;
    }

    /// <inheritdoc/>
    public async Task<IdentityResult> AssignRoleToUserAsync(AssignRoleRequest assignRoleRequest)
    {
        IdentityResult result = new();
        var user = await _userManager.FindByEmailAsync(assignRoleRequest.Email);
        if (user != null)
        {
            if (!_roleManager.RoleExistsAsync(assignRoleRequest.RoleName.ToUpper()).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(assignRoleRequest.RoleName.ToUpper())).GetAwaiter().GetResult();
            }
            return await _userManager.AddToRoleAsync(user, assignRoleRequest.RoleName.ToUpper());
        }
        return result;

    }

    /// <inheritdoc/>
    public async Task<string> LoginUserAsync(LoginRequest loginRequest)
    {
        var user = await _userManager.FindByEmailAsync(loginRequest.Email);
        Employee? employee = await _employeeService.GetEmployeeByEmailAsync(loginRequest.Email);
        if (user is null && !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
        {
            return string.Empty;
        }

        return _jwtTokenGenerator.GenerateToken(
                user,
                await _userManager.GetRolesAsync(user),
                employee
                );
    }

    /// <inheritdoc/>
    public async Task<IdentityResult> RegisterUserAsync(RegistrationRequest registrationRequest)
    {
        return await _userManager.CreateAsync(
            new IdentityUser { UserName = registrationRequest.Email, Email = registrationRequest.Email },
            registrationRequest.Password);
    }
}
