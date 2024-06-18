using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGaHRMS.ApiService.Interfaces;
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
    public async Task<IActionResult> Register(RegistrationRequest registrationRequest)
    {

        var errorMessage = await _authService.Register(registrationRequest);
        if (!string.IsNullOrEmpty(errorMessage))
        {
            _response.IsSuccess = false;
            _response.Message = errorMessage;
            return BadRequest(_response);
        }
        return Ok(_response);
    }

    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest logInRequest)
    {
        var loginResponse = await _authService.Login(logInRequest);
        if (loginResponse.User == null)
        {
            _response.IsSuccess = false;
            _response.Message = "Username or password is incorrect";
            return Unauthorized();
        }
        _response.Result = loginResponse;
        return Ok(_response);

    }

    [HttpPost("createrole")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> CreateRole([FromBody] string roleName)
    {
        var roleCreated = await _authService.CreateRole(roleName);
        if (!roleCreated)
        {
            _response.IsSuccess = false;
            _response.Message = "Role creation failed or role already exists.";
            return BadRequest(_response);
        }
        _response.IsSuccess = true;
        _response.Message = "Role created successfully.";
        return Ok(_response);
    }

    [HttpPost("AssignRole")]
    public async Task<IActionResult> AssignRole(RegistrationRequest model)
    {
        var isAssignRoleSuccess = await _authService.AssignRole(model.Email, model?.Role.ToUpper());
        if (!isAssignRoleSuccess)
        {
            _response.IsSuccess = false;
            _response.Message = "Error encountered";
            return BadRequest(_response);
        }
        return Ok(_response);

    }
}