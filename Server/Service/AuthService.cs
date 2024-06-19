using Microsoft.AspNetCore.Identity;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.DataContext;
using SiGaHRMS.Data.Entities.Api;


namespace SiGaHRMS.ApiService.Service;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(AppDbContext db, IJwtTokenGenerator jwtTokenGenerator,
        UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _db = db;
        _jwtTokenGenerator = jwtTokenGenerator;
        _userManager = userManager;
        _roleManager = roleManager;
    }

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

    public async Task<string> LoginUserAsync(LoginRequest loginRequest)
    {
        var user = await _userManager.FindByEmailAsync(loginRequest.Email);
        if (user is null && !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
        {
            return string.Empty;
        }

        return _jwtTokenGenerator.GenerateToken(
                user,
                await _userManager.GetRolesAsync(user));
    }

    public async Task<IdentityResult> RegisterUserAsync(RegistrationRequest registrationRequest)
    {
        var user = new IdentityUser { UserName = registrationRequest.Email, Email = registrationRequest.Email };
        return await _userManager.CreateAsync(user, registrationRequest.Password);
    }
}
