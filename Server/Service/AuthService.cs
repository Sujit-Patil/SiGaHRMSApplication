using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.DataContext;
using SiGaHRMS.Data.Model.AuthModel;
using SiGaHRMS.Data.Model.Dto;
using IdentityUser = Microsoft.AspNetCore.Identity.IdentityUser;


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
    public async Task<bool> CreateRole(string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            return result.Succeeded;
        }
        return false;
    }
    public async Task<bool> AssignRole(string email, string roleName)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
            {
                //create role if it does not exist
                _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
            }
            await _userManager.AddToRoleAsync(user, roleName);
            return true;
        }
        return false;

    }

    public async Task<string> Login(string email,string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null && await _userManager.CheckPasswordAsync(user, password))
        {
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);
            return  token;
        }
        return "Token Is not Genarated";
    }

    public async Task<string> Register(RegisterModel registrationRequestDto)
    {
        var user = new IdentityUser { UserName = registrationRequestDto.Email, Email = registrationRequestDto.Email };
        var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
        if (result.Succeeded)
        {
            return "User created successfully";
        }

        return "User Not Created";
    }

}
