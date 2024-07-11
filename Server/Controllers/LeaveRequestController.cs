using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.ApiService.Service;
using SiGaHRMS.Data.Constants;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Dto;
using SiGaHRMS.Data.Validations;

namespace SiGaHRMS.ApiService.Controllers;

/// <summary>
/// LeaveRequest Controller 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class LeaveRequestController : ControllerBase
{
    private readonly ILeaveRequestService _leaveRequestService;
    private readonly ISessionService _sessionService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private ILogger<LeaveRequestController> _logger;
    private ValidationResult validationResult;

    /// <summary>
    /// Initializes a new instance of see ref<paramref name="LeaveRequestController"/>
    /// </summary>
    /// <param name="leaveRequestService"></param>
    public LeaveRequestController(ILeaveRequestService leaveRequestService, ISessionService sessionService, IDateTimeProvider dateTimeProvider, ILogger<LeaveRequestController> logger)
    {
        _leaveRequestService = leaveRequestService;
        _sessionService = sessionService;
        _dateTimeProvider = dateTimeProvider;
        _logger = logger;
        validationResult = new();
    }

    /// <summary>
    /// The controller method to retrive all LeaveRequests.
    /// </summary>
    /// <returns>returns list of LeaveRequests</returns>

    [HttpGet]
    [Authorize(Roles = RoleConstants.SUPERADMIN)]
    public List<LeaveRequest> GetAllLeaveRequests()
    {
        return _leaveRequestService.GetAllLeaveRequests();
    }

    /// <summary>
    /// Get method to retrive single LeaveRequest
    /// </summary>
    /// <param name="id">LeaveRequest Id</param>
    /// <returns> return single LeaveRequest using LeaveRequest Id</returns>
    [HttpGet("{id:int}")]
    public async Task<LeaveRequest?> GetLeaveRequestByIdAsync(int id)
    {
        return await _leaveRequestService.GetLeaveRequestByIdAsync(id);
    }

    /// <summary>
    /// Post method to Add LeaveRequest to database
    /// </summary>
    /// <param name="leaveRequest"> LeaveRequest object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPost]
    public async Task<IActionResult> AddLeaveRequestAsync(LeaveRequest leaveRequest)
    {
        try
        {
            if (!IsValidLeaveRequest(leaveRequest))
            {
                return BadRequest(validationResult);
            }
            await _leaveRequestService.AddLeaveRequestAsync(leaveRequest);

            return Ok(validationResult);
        }

        catch (Exception ex)
        {
            _logger.LogInformation($"[AddLeaveRequestAsync] Error Occurs : {ex.Message}");
            validationResult.AddErrorMesageCode(UserActionConstants.UnExpectedException, UserActionConstants.ErrorDescriptions);
            return BadRequest(validationResult);
        }
    }

    /// <summary>
    /// Get method to retrive LeaveRequest By Date
    /// </summary>
    /// <param name="LeaveRequest Dto">leaveRequestDto</param>
    /// <returns> return List of Leave using Date</returns>
    [HttpPost("ByDate")]
    public List<LeaveRequest> GetLeaveByDateAsync(RequestDto leaveRequestDto)
    {
        return _leaveRequestService.GetLeaveRequestsByDateAsync(leaveRequestDto);
    }

    /// <summary>
    /// Upadte method to Update LeaveRequest to database
    /// </summary>
    /// <param name="leaveRequest">LeaveRequest object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateLeaveRequestAsync(LeaveRequest leaveRequest)
    {
        try
        {
            if (!IsValidLeaveRequest(leaveRequest))
            {
                return BadRequest(validationResult);
            }
            await _leaveRequestService.UpdateLeaveRequestAsync(leaveRequest);
            return Ok(validationResult);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[UpdateLeaveRequestAsync] Error Occurs : {ex.Message}");
            validationResult.AddErrorMesageCode(UserActionConstants.UnExpectedException, UserActionConstants.ErrorDescriptions);
            return BadRequest(validationResult);
        }
    }

    /// <summary>
    /// Delete method to delete LeaveRequest to database
    /// </summary>
    /// <param name="id">LeaveRequest Id</param>
    /// <returns>returns bool</returns>
    [HttpDelete("{id:int}")]
    public async Task<bool> DeleteLeaveRequestAsync(int id)
    {
        await _leaveRequestService.DeleteLeaveRequestAsync(id);
        return true;
    }

    #region Private Methods
    private bool IsValidLeaveRequest(LeaveRequest leaveRequest)
    {
        var currentEmployeeId = _sessionService.GetCurrentEmployeeId();

        if (leaveRequest.EmployeeId != currentEmployeeId)
        {
            validationResult.AddErrorMesageCode(UserActionConstants.UnAuthorizedRequest, UserActionConstants.ErrorDescriptions);
            return false;
        }

        var today = _dateTimeProvider.Today;

        if (today > leaveRequest.FromDate || today > leaveRequest.ToDate || leaveRequest.FromDate > leaveRequest.ToDate)
        {
            validationResult.AddErrorMesageCode(UserActionConstants.RequestInValid, UserActionConstants.ErrorDescriptions);
            return false;
        }

        return true;
    }

    #endregion
}
