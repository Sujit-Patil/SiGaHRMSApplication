using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.ApiService.Service;
using SiGaHRMS.Data.Constants;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Dto;
using SiGaHRMS.Data.Validations;
using System.Text;

namespace SiGaHRMS.ApiService.Controllers;

/// <summary>TimeSheetDetailController
/// TimeSheetDetail Controller 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TimeSheetDetailController : ControllerBase
{
    private readonly ITimeSheetDetailService _timeSheetDetailService;
    private readonly ISessionService _sessionService;
    private ILogger<TimeSheetDetailController> _logger;
    private ValidationResult validationResult;


    /// <summary>
    /// Initializes a new instance of see ref<paramref name="timeSheetDetailController"/>
    /// </summary>
    /// <param name="TimeSheetDetailService"></param>
    public TimeSheetDetailController(ITimeSheetDetailService timeSheetDetailService, ISessionService sessionService, ILogger<TimeSheetDetailController> logger)
    {
        _timeSheetDetailService = timeSheetDetailService;
        _sessionService = sessionService;
        _logger = logger;
        validationResult = new();

    }

    /// <summary>
    /// The controller method to retrive all TimeSheetDetails.
    /// </summary>
    /// <returns>returns list of TimeSheetDetails</returns>

    [HttpGet]
    [Authorize(Roles = RoleConstants.SUPERADMIN)]
    public Task<IEnumerable<TimeSheetDetail>> GetAllTimeSheetDetails()
    {
        return _timeSheetDetailService.GetAllTimeSheetDetails();
    }

    /// <summary>
    /// Get method to retrive Attendance By Date
    /// </summary>
    /// <param name="AttendanceDto">attendanceDto</param>
    /// <returns> return List of Attendance using Date</returns>
    [HttpPost("ByDate")]
    public List<TimeSheetDetail> GetTimeSheetDetailByDateAsync(RequestDto timesheetDetailDto)
    {

        return _timeSheetDetailService.GetTimesheetDetailByDateAsync(timesheetDetailDto);
    }

    /// <summary>
    /// Get method to retrive single TimeSheetDetail
    /// </summary>
    /// <param name="id">timeSheetDetail Id</param>
    /// <returns> return single TimeSheetDetail using timeSheetDetail Id</returns>
    [HttpGet("{id:int}")]
    public async Task<TimeSheetDetail?> GetTimeSheetDetailByIdAsync(int id)
    {
        return await _timeSheetDetailService.GetTimeSheetDetailByIdAsync(id);
    }

    /// <summary>
    /// Post method to Add TimeSheetDetail to database
    /// </summary>
    /// <param name="TimeSheetDetail"> TimeSheetDetail object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddTimeSheetDetailAsync([FromBody] TimeSheetDetail timeSheetDetail)
    {
        try
        {
            await _timeSheetDetailService.AddTimeSheetDetailAsync(timeSheetDetail);
            return Ok(validationResult);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[UpdateTimeSheetDetailAsync] Error Occurs : {ex.Message}");
            validationResult.AddErrorMesageCode(UserActionConstants.UnExpectedException, UserActionConstants.ErrorDescriptions);
            return BadRequest(validationResult);
        }
    }

    /// <summary>
    /// Upadte method to Update TimeSheetDetail to database
    /// </summary>
    /// <param name="TimeSheetDetail">TimeSheetDetail object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateTimeSheetDetailAsync(TimeSheetDetail timeSheetDetail)
    {
        try
        {
            if (!IsValidLeaveRequest(timeSheetDetail))
            {
                return BadRequest(validationResult);
            }
            await _timeSheetDetailService.UpdateTimeSheetDetailAsync(timeSheetDetail);

            return Ok(validationResult);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[UpdateTimeSheetDetailAsync] Error Occurs : {ex.Message}");
            validationResult.AddErrorMesageCode(UserActionConstants.UnExpectedException, UserActionConstants.ErrorDescriptions);
            return BadRequest(validationResult);
        }

    }

    /// <summary>
    /// Delete method to delete TimeSheetDetail to database
    /// </summary>
    /// <param name="id">timeSheetDetail Id</param>
    /// <returns>returns bool</returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTimeSheetDetailAsync(int id)
    {
        try
        {
            await _timeSheetDetailService.DeleteTimeSheetDetailAsync(id);
            return Ok(validationResult);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[DeleteTimeSheetDetailAsync] Error Occurs : {ex.Message}");
            validationResult.AddErrorMesageCode(UserActionConstants.TimeSheetDeleteFailed, UserActionConstants.ErrorDescriptions);
            return BadRequest(validationResult);
        }
    }

    #region Private Methods
    private bool IsValidLeaveRequest(TimeSheetDetail timeSheetDetailRequest)
    {
        var currentEmployeeId = _sessionService.GetCurrentEmployeeId();

        if (timeSheetDetailRequest.Timesheet?.EmployeeId != currentEmployeeId)
        {
            validationResult.AddErrorMesageCode(UserActionConstants.UnAuthorizedRequest, UserActionConstants.ErrorDescriptions);
            return false;
        }

        if (timeSheetDetailRequest.TimesheetId == null || timeSheetDetailRequest.HoursSpent < 0)
        {
            validationResult.AddErrorMesageCode(UserActionConstants.RequestInValid, UserActionConstants.ErrorDescriptions);
            return false;
        }

        return true;
    }

    #endregion
}
