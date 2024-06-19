using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Model.AuthModel;
using SiGaHRMS.Data.Model.Dto;


namespace SiGaHRMS.ApiService.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthAPIController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _configuration;
    protected ResponseDto _response;
    public AuthAPIController(IAuthService authService, IConfiguration configuration)
    {
        _authService = authService;
        _configuration = configuration;
        _response = new();
    }



    [HttpPost("register")]
    [Authorize(Roles = "Super Admin")]
    public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
    {

        var errorMessage = await _authService.Register(registerModel);
        if (!errorMessage.Succeeded)
        {
            _response.IsSuccess = false;
            _response.Message = string.Join("; ", errorMessage.Errors.Select(e => e.Description));
            return BadRequest(_response);
        }
        return Ok(_response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        var loginResponse = await _authService.Login(loginModel);
        if (loginResponse == null)
        {
            _response.IsSuccess = false;
            _response.Message = "Username or password is incorrect";
            return BadRequest(_response);
        }
        _response.Result = loginResponse;
        return Ok(_response);

    }

    [HttpPost("create-role")]
    [Authorize(Roles = "Super Admin")]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleModel createRoleModel)
    {
        var roleCreated = await _authService.CreateRole(createRoleModel);
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
    [Authorize(Roles = "Super Admin")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleModel assignRoleModel)
    {
        var assignRoleSuccessful = await _authService.AssignRole(assignRoleModel);
        if (!assignRoleSuccessful.Succeeded)
        {
            _response.IsSuccess = false;
            _response.Message = string.Join("; ", assignRoleSuccessful.Errors.Select(e => e.Description));
            return BadRequest(_response);
        }
        return Ok(_response);

    }

}