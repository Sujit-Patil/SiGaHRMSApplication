using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.ApiService.Service;
using SiGaHRMS.Data.Constants;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Validations;

namespace SiGaHRMS.ApiService.Controllers;

/// <summary>IncentivePurposeController
/// IncentivePurpose Controller 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class IncentivePurposeController : ControllerBase
{
    private readonly IIncentivePurposeService _incentivePurposeService;
    private readonly ILogger<IncentivePurposeController> _logger;
    private readonly ISessionService _sessionService;
    private ValidationResult validationResult;

    /// <summary>
    /// Initializes a new instance of see ref<paramref name="IncentivePurposeController"/>
    /// </summary>
    /// <param name="incentivePurposeService"></param>
    public IncentivePurposeController(IIncentivePurposeService incentivePurposeService, ILogger<IncentivePurposeController> logger, ISessionService sessionService)
    {
        _incentivePurposeService = incentivePurposeService;
        _logger = logger;
        _sessionService = sessionService;
        validationResult = new ValidationResult();
    }

    /// <summary>
    /// The controller method to retrive all IncentivePurposes.
    /// </summary>
    /// <returns>returns list of IncentivePurposes</returns>
    
    [HttpGet]
    [Authorize(Roles = RoleConstants.SUPERADMIN + "," + RoleConstants.HR)]
    public Task<IEnumerable<IncentivePurpose>> GetAllIncentivePurposes()
    {
        return _incentivePurposeService.GetAllIncentivePurposes();
    }

    /// <summary>
    /// Get method to retrive single IncentivePurpose
    /// </summary>
    /// <param name="id">IncentivePurpose Id</param>
    /// <returns> return single IncentivePurpose using IncentivePurpose Id</returns>
    [HttpGet("{id:int}")]
    public async Task<IncentivePurpose?> GetIncentivePurposeByIdAsync(int id)
    {
        return await _incentivePurposeService.GetIncentivePurposeByIdAsync(id);
    }

    /// <summary>
    /// Post method to Add IncentivePurpose to database
    /// </summary>
    /// <param name="incentivePurpose"> IncentivePurpose object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPost]
    [Authorize(Roles = RoleConstants.SUPERADMIN + "," + RoleConstants.HR)]
    public async Task<IActionResult> AddIncentivePurposeAsync(IncentivePurpose incentivePurpose)
    {
        try
        {
            await _incentivePurposeService.AddIncentivePurposeAsync(incentivePurpose);
            return Ok(validationResult);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[AddIncentivePurposeAsync] Error Occurs : {ex.Message}");
            validationResult.AddErrorMesageCode(UserActionConstants.UnExpectedException, UserActionConstants.ErrorDescriptions);
            return BadRequest(validationResult);
        }
    }

    /// <summary>
    /// Upadte method to Update IncentivePurpose to database
    /// </summary>
    /// <param name="incentivePurpose">IncentivePurpose object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPut]
    [Authorize(Roles = RoleConstants.SUPERADMIN + "," + RoleConstants.HR)]
    public async Task<IActionResult> UpdateIncentivePurposeAsync(IncentivePurpose incentivePurpose)
    {
        try
        {
            await _incentivePurposeService.UpdateIncentivePurposeAsync(incentivePurpose);
            return Ok(validationResult);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[UpdateIncentivePurposeAsync] Error Occurs : {ex.Message}");
            validationResult.AddErrorMesageCode(UserActionConstants.UnExpectedException, UserActionConstants.ErrorDescriptions);
            return BadRequest(validationResult);
        }
    }

    /// <summary>
    /// Delete method to delete IncentivePurpose to database
    /// </summary>
    /// <param name="id">IncentivePurpose Id</param>
    /// <returns>returns bool</returns>
    [HttpDelete("{id:int}")]
    public async Task<bool> DeleteIncentivePurposeAsync(int id)
    {
        await _incentivePurposeService.DeleteIncentivePurposeAsync(id);
        return true;
    }
}
