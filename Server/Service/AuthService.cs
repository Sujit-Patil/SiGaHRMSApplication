using Microsoft.AspNetCore.Identity;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.DataContext;
using SiGaHRMS.Data.Entities.Api;
using SiGaHRMS.Data.Model.AuthModel;


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
    public async Task<IdentityResult> CreateRole(CreateRoleModel createRoleModel)
    {
        IdentityResult result = new();

        if (!await _roleManager.RoleExistsAsync(createRoleModel.RoleName))
        {
            result = await _roleManager.CreateAsync(new IdentityRole(createRoleModel.RoleName));
            return result;
        }
        return result;
    }

    public async Task<IdentityResult> AssignRole(AssignRoleModel assignRoleModel)
    {
        IdentityResult result = new();
        var user = await _userManager.FindByEmailAsync(assignRoleModel.Email);
        if (user != null)
        {
            if (!_roleManager.RoleExistsAsync(assignRoleModel.RoleName.ToUpper()).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(assignRoleModel.RoleName.ToUpper())).GetAwaiter().GetResult();
            }
            return await _userManager.AddToRoleAsync(user, assignRoleModel.RoleName.ToUpper());
        }
        return result;

    }

    public async Task<string> Login(LoginModel loginModel)
    {
        var user = await _userManager.FindByEmailAsync(loginModel.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
        {
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);
            return token;
        }
        return "Token Is not Genarated";
    }

    public async Task<IdentityResult> Register(RegisterModel registerModel)
    {
        IdentityResult result = new();
        var user = new IdentityUser { UserName = registerModel.Email, Email = registerModel.Email };
        result = await _userManager.CreateAsync(user, registerModel.Password);
        return result;
    }

}
