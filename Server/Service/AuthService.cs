using Microsoft.AspNetCore.Identity;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.DataContext;
using SiGaHRMS.Data.Entities.Api;
using SiGaHRMS.Data.Model.AuthModel;
using SiGaHRMS.Data.Model.Dto;


namespace SiGaHRMS.ApiService.Service;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(AppDbContext db, IJwtTokenGenerator jwtTokenGenerator,
        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
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
        var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
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

    public async Task<LoginResponseDto> Login(LoginRequest loginRequest)
    {
        var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequest.UserName.ToLower());

        bool isValid = await _userManager.CheckPasswordAsync(user, loginRequest.Password);

        if (user == null || isValid == false)
        {
            return new LoginResponseDto() { User = null, Token = "" };
        }

        //if user was found , Generate JWT Token
        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtTokenGenerator.GenerateToken(user, roles);

        UserDto userDTO = new()
        {
            Email = user.Email,
            ID = user.Id,
            Name = user.Name,
            PhoneNumber = user.PhoneNumber
        };

       return new LoginResponseDto()
        {
            User = userDTO,
            Token = token
        };
    }

    public async Task<string> Register(RegistrationRequest registrationRequest)
    {
        ApplicationUser user = new()
        {
            UserName = registrationRequest.Email,
            Email = registrationRequest.Email,
            NormalizedEmail = registrationRequest.Email.ToUpper(),
            Name = registrationRequest.Name,
            PhoneNumber = registrationRequest.PhoneNumber
        };

        try
        {
            var result = await _userManager.CreateAsync(user, registrationRequest.Password);
            if (result.Succeeded)
            {
                var userToReturn = _db.ApplicationUsers.First(u => u.UserName == registrationRequest.Email);

                UserDto userDto = new()
                {
                    Email = userToReturn.Email,
                    ID = userToReturn.Id,
                    Name = userToReturn.Name,
                    PhoneNumber = userToReturn.PhoneNumber
                };

                return "";

            }
            else
            {
                return result.Errors.FirstOrDefault().Description;
            }

        }
        catch (Exception ex)
        {

        }
        return "Error Encountered";
    }
}
