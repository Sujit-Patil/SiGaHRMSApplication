using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.ApiService.Service;
using SiGaHRMS.Data.Constants;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Dto;
using SiGaHRMS.Data.Validations;

namespace SiGaHRMS.ApiService.Controllers;

/// <summary>IncentiveController
/// Incentive Controller 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class IncentiveController : ControllerBase
{
    private readonly IIncentiveService _incentiveService;
    private readonly ILogger<IncentiveController> _logger;
    private readonly ISessionService _sessionService;
    private ValidationResult validationResult;

    /// <summary>
    /// Initializes a new instance of see ref<paramref name="IncentiveController"/>
    /// </summary>
    /// <param name="incentiveService"></param>
    public IncentiveController(IIncentiveService incentiveService, ILogger<IncentiveController> logger, ISessionService sessionService)
    {
        _incentiveService = incentiveService;
        _logger = logger;
        _sessionService = sessionService;
        validationResult = new ValidationResult();
    }

    /// <summary>
    /// The controller method to retrive all Incentives.
    /// </summary>
    /// <returns>returns list of Incentives</returns>
    
    [HttpGet]
    [Authorize(Roles = RoleConstants.SUPERADMIN + "," + RoleConstants.HR)]
    public Task<IEnumerable<Incentive>> GetAllIncentives()
    {
        return _incentiveService.GetAllIncentives();
    }

    /// <summary>
    /// Get method to retrive single Incentive
    /// </summary>
    /// <param name="id">Incentive Id</param>
    /// <returns> return single Incentive using Incentive Id</returns>
    [HttpGet("{id:int}")]
    public async Task<Incentive?> GetIncentiveByIdAsync(int id)
    {
        return await _incentiveService.GetIncentiveByIdAsync(id);
    }

    /// <summary>
    /// Post method to Add Incentive to database
    /// </summary>
    /// <param name="incentive"> Incentive object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPost]
    [Authorize(Roles = RoleConstants.SUPERADMIN + "," + RoleConstants.HR)]
    public async Task<IActionResult> AddIncentiveAsync(Incentive incentive)
    {
        try
        {
            if (!IsValidLeaveRequest(incentive))
            {
                return BadRequest(validationResult);
            }
            await _incentiveService.AddIncentiveAsync(incentive);
            return Ok(validationResult);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[AddIncentiveAsync] Error Occurs : {ex.Message}");
            validationResult.AddErrorMesageCode(UserActionConstants.UnExpectedException, UserActionConstants.ErrorDescriptions);
            return BadRequest(validationResult);
        }
    }


    /// <summary>
    /// Get method to retrive Attendance By Date
    /// </summary>
    /// <param name="holidayRequestDto">RequestDto</param>
    /// <returns> return List of Holiday using Date</returns>
    [HttpPost("ByDate")]
    public async Task<List<Incentive>> GetIncentivesByDateAsync(RequestDto holidayRequestDto)
    {
        return await _incentiveService.GetIncentivesByDateAsync(holidayRequestDto);
    }

    /// <summary>
    /// Upadte method to Update Incentive to database
    /// </summary>
    /// <param name="incentive">Incentive object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPut]
    [Authorize(Roles = RoleConstants.SUPERADMIN + "," + RoleConstants.HR)]
    public async Task<IActionResult> UpdateIncentiveAsync(Incentive incentive)
    {
        try
        {
            if (!IsValidLeaveRequest(incentive))
            {
                return BadRequest(validationResult);
            }
            await _incentiveService.UpdateIncentiveAsync(incentive);
            return Ok(validationResult);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[UpdateIncentiveAsync] Error Occurs : {ex.Message}");
            validationResult.AddErrorMesageCode(UserActionConstants.UnExpectedException, UserActionConstants.ErrorDescriptions);
            return BadRequest(validationResult);
        }
    }

    /// <summary>
    /// Delete method to delete Incentive to database
    /// </summary>
    /// <param name="id">Incentive Id</param>
    /// <returns>returns bool</returns>
    [HttpDelete("{id:int}")]
    public async Task<bool> DeleteIncentiveAsync(int id)
    {
        await _incentiveService.DeleteIncentiveAsync(id);
        return true;
    }

    #region Private Methods
    private bool IsValidLeaveRequest(Incentive incentive)
    {
        var currentEmployeeId = _sessionService.GetCurrentEmployeeId();

        if (incentive.EmployeeId == currentEmployeeId)
        {
            validationResult.AddErrorMesageCode(UserActionConstants.UnAuthorizedRequest, UserActionConstants.ErrorDescriptions);
            return false;
        }

        return true;
    }

    #endregion
}
