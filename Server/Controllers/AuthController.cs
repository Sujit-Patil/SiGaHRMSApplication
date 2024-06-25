using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Constants;
using SiGaHRMS.Data.Entities.Api;
using SiGaHRMS.Data.Model.Dto;


namespace SiGaHRMS.ApiService.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    protected ResponseDto _response;
    public AuthController(IAuthService authService, IConfiguration configuration)
    {
        _authService = authService;
        _response = new();
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync([FromBody] RegistrationRequest registrationRequest)
    {

        var errorMessage = await _authService.RegisterUserAsync(registrationRequest);
        if (!errorMessage.Succeeded)
        {
            _response.IsSuccess = false;
            _response.Message = string.Join("; ", errorMessage.Errors.Select(e => e.Description));
            return BadRequest(_response);
        }
        return Ok(_response);
    }

    
    [HttpPost("login")]
    public async Task<IActionResult> LoginUserAsync([FromBody] LoginRequest loginRequest)
    {
        var loginResponse = await _authService.LoginUserAsync(loginRequest);
        if (loginResponse == null)
        {
            _response.IsSuccess = false;
            _response.Message = "Username or password is incorrect";
            return Unauthorized();
        }
        _response.Result = loginResponse;
        return Ok(_response);

    }

    [HttpPost("create-role")]
    public async Task<IActionResult> CreateUserRoleAsync(string roleToCreate)
    {
        var roleCreated = await _authService.CreateUserRoleAsync(roleToCreate);
        if (!roleCreated.Succeeded)
        {
            _response.IsSuccess = false;
            _response.Message = string.Join("; ", roleCreated.Errors.Select(e => e.Description));
            return BadRequest(_response);
        }
        _response.IsSuccess = true;
        _response.Message = "Role created successfully.";
        return Ok(_response);
    }

    [HttpPost("AssignRole")]
    public async Task<IActionResult> AssignRoleToUserAsync([FromBody] AssignRoleRequest assignRoleModel)
    {
        var assignRoleSuccessful = await _authService.AssignRoleToUserAsync(assignRoleModel);
        if (!assignRoleSuccessful.Succeeded)
        {
            _response.IsSuccess = false;
            _response.Message = string.Join("; ", assignRoleSuccessful.Errors.Select(e => e.Description));
            return BadRequest(_response);
        }
        return Ok(_response);

    }
}