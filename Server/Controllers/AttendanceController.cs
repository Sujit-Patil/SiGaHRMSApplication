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
/// Attendance Controller 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceService _attendanceService;
    private readonly ISessionService _sessionService;
    private ILogger<AttendanceController> _logger;
    private readonly ValidationResult validationResult;

    /// <summary>
    /// Initializes a new instance of see ref<paramref name="AttendanceController"/>
    /// </summary>
    /// <param name="attendanceService"></param>
    public AttendanceController(IAttendanceService attendanceService, ISessionService sessionService, ILogger<AttendanceController> logger)
    {
        _attendanceService = attendanceService;
        _sessionService = sessionService;
        _logger = logger;
        validationResult = new();
    }

    /// <summary>
    /// The controller method to retrive all Attendances.
    /// </summary>
    /// <returns>returns list of Attendances</returns>

    [HttpGet]
    [Authorize(Roles = RoleConstants.SUPERADMIN)]
    public Task<IEnumerable<Attendance>> GetAllAttendances()
    {
        return _attendanceService.GetAllAttendances();
    }

    /// <summary>
    /// Get method to retrive single Attendance
    /// </summary>
    /// <param name="id">Attendance Id</param>
    /// <returns> return single Attendance using Attendance Id</returns>
    [HttpGet("{id:int}")]
    public async Task<Attendance?> GetAttendanceByIdAsync(int id)
    {
        return await _attendanceService.GetAttendanceByIdAsync(id);
    }

    /// <summary>
    /// Get method to retrive Attendance By Date
    /// </summary>
    /// <param name="AttendanceDto">attendanceDto</param>
    /// <returns> return List of Attendance using Date</returns>
    [HttpPost("ByDate")]
    public List<Attendance> GetAttendanceByDateAsync(RequestDto attendanceDto)
    {
        return _attendanceService.GetAttendanceByDateAsync(attendanceDto);
    }

    /// <summary>
    /// Post method to Add Attendance to database
    /// </summary>
    /// <param name="attendance"> Attendance object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPost]
    public async Task<IActionResult> AddAttendanceAsync(Attendance attendance)
    {
        try
        {
            if (!IsValidLeaveRequest(attendance))
            {
                return Unauthorized(validationResult);
            }

            await _attendanceService.AddAttendanceAsync(attendance);

            return Ok(validationResult);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[AddAttendanceAsync] Error Occurs : {ex.Message}");
            validationResult.AddErrorMesageCode(UserActionConstants.UnExpectedException, UserActionConstants.ErrorDescriptions);
            return BadRequest(validationResult);
        }
    }

    /// <summary>
    /// Upadte method to Update Attendance to database
    /// </summary>
    /// <param name="attendance">Attendance object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateAttendanceAsync(Attendance attendance)
    {
        try
        {
            if (!IsValidLeaveRequest(attendance))
            {
                return Unauthorized(validationResult);
            }

            await _attendanceService.UpdateAttendanceAsync(attendance);

            return Ok(validationResult);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[UpdateAttendanceAsync] Error Occurs : {ex.Message}");
            validationResult.AddErrorMesageCode(UserActionConstants.UnExpectedException, UserActionConstants.ErrorDescriptions);
            return BadRequest(validationResult);
        }

    }

    /// <summary>
    /// Delete method to delete Attendance to database
    /// </summary>
    /// <param name="id">Attendance Id</param>
    /// <returns>returns bool</returns>
    [HttpDelete("{id:int}")]
    public async Task<bool> DeleteAttendanceAsync(int id)
    {
        await _attendanceService.DeleteAttendanceAsync(id);
        return true;
    }

    #region Private Methods
    private bool IsValidLeaveRequest(Attendance attendanceRequest)
    {
        var currentEmployeeId = _sessionService.GetCurrentEmployeeId();

        if (attendanceRequest.EmployeeId != currentEmployeeId)
        {
            validationResult.AddErrorMesageCode(UserActionConstants.UnAuthorizedRequest, UserActionConstants.ErrorDescriptions);
            return false;
        }

        return true;
    }

    #endregion
}
