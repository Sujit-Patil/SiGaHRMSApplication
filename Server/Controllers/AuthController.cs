using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SiGaHRMS.Data.Model.AuthModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SiGaHRMS.ApiService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [Authorize("Admin")]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var user = new IdentityUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            return Ok(new { Result = "User created successfully" });
        }
        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var roles = await _userManager.GetRolesAsync(user);
            var token = GenerateJwtToken(user, roles);
            return Ok(new { Token = token });
        }

        return Unauthorized();
    }

    private string GenerateJwtToken(IdentityUser user, IList<string> roles)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            // Include roles as claims
            new Claim(ClaimTypes.Role, string.Join(",", roles)),
            }),
            Expires = DateTime.UtcNow.AddDays(7), // Token expiration
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }


    [Authorize]
    [HttpPost("createrole")]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleModel model)
    {
        var roleExist = await _roleManager.RoleExistsAsync(model.RoleName);
        if (!roleExist)
        {
            var roleResult = await _roleManager.CreateAsync(new IdentityRole(model.RoleName));
            if (roleResult.Succeeded)
            {
                return Ok(new { Result = "Role created successfully" });
            }
            return BadRequest(roleResult.Errors);
        }
        return BadRequest(new { Error = "Role already exists" });
    }
    [Authorize("Admin")]
    [HttpPost("assignrole")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return BadRequest(new { Error = "User not found" });
        }
        var roleResult = await _userManager.AddToRoleAsync(user, model.RoleName);
        if (roleResult.Succeeded)
        {
            return Ok(new { Result = "Role assigned successfully" });
        }
        return BadRequest(roleResult.Errors);
    }
}


