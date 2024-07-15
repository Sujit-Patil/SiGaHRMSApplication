using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Constants;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Validations;

namespace SiGaHRMS.ApiService.Controllers;

/// <summary>
/// LeaveBalance Controller 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class LeaveBalanceController : ControllerBase
{
    private readonly ILeaveBalanceService _leaveBalanceService;
    private ILogger<LeaveBalanceController> _logger;
    private ValidationResult validationResult;

    /// <summary>
    /// Initializes a new instance of see ref<paramref name="LeaveBalanceController"/>
    /// </summary>
    /// <param name="leaveBalanceService"></param>
    public LeaveBalanceController(ILeaveBalanceService leaveBalanceService, ILogger<LeaveBalanceController> logger)
    {
        _leaveBalanceService = leaveBalanceService;
        _logger = logger;
        validationResult = new();
    }

    /// <summary>
    /// The controller method to retrive all LeaveBalances.
    /// </summary>
    /// <returns>returns list of LeaveBalances</returns>

    [HttpGet]
    [Authorize(Roles = RoleConstants.SUPERADMIN)]
    public Task<IEnumerable<LeaveBalance>> GetAllLeaveBalances()
    {
        return _leaveBalanceService.GetAllLeaveBalances();
    }

    /// <summary>
    /// Get method to retrive single LeaveBalance
    /// </summary>
    /// <param name="id">LeaveBalance Id</param>
    /// <returns> return single LeaveBalance using LeaveBalance Id</returns>
    [HttpGet("{id:int}")]
    public async Task<LeaveBalance?> GetLeaveBalanceByIdAsync(int id)
    {
        return await _leaveBalanceService.GetLeaveBalanceByIdAsync(id);
    }

    /// <summary>
    /// Post method to Add LeaveBalance to database
    /// </summary>
    /// <param name="leaveBalance"> LeaveBalance object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPost]
    [Authorize(Roles = RoleConstants.SUPERADMIN+","+RoleConstants.HR)]
    public async Task<IActionResult> AddLeaveBalanceAsync(LeaveBalance leaveBalance)
    {
        try
        {
            await _leaveBalanceService.AddLeaveBalanceAsync(leaveBalance);
            return Ok(validationResult);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[AddLeaveBalanceAsync] Error Occurs : {ex.Message}");
            validationResult.AddErrorMesageCode(UserActionConstants.UnExpectedException, UserActionConstants.ErrorDescriptions);
            return BadRequest(validationResult);
        }

    }

    /// <summary>
    /// Upadte method to Update LeaveBalance to database
    /// </summary>
    /// <param name="leaveBalance">LeaveBalance object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateLeaveBalanceAsync(LeaveBalance leaveBalance)
    {
        try
        {
            await _leaveBalanceService.UpdateLeaveBalanceAsync(leaveBalance);
            return Ok(validationResult);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[UpdateLeaveBalanceAsync] Error Occurs : {ex.Message}");
            validationResult.AddErrorMesageCode(UserActionConstants.UnExpectedException, UserActionConstants.ErrorDescriptions);
            return BadRequest(validationResult);
        }
    }

    /// <summary>
    /// Delete method to delete LeaveBalance to database
    /// </summary>
    /// <param name="id">LeaveBalance Id</param>
    /// <returns>returns bool</returns>
    [HttpDelete("{id:int}")]
    public async Task<bool> DeleteLeaveBalanceAsync(int id)
    {
        await _leaveBalanceService.DeleteLeaveBalanceAsync(id);
        return true;
    }
}
