using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGaHRMS.ApiService.Interfaces;
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
    private ValidationResult validationResult;


    /// <summary>
    /// Initializes a new instance of see ref<paramref name="timeSheetDetailController"/>
    /// </summary>
    /// <param name="TimeSheetDetailService"></param>
    public TimeSheetDetailController(ITimeSheetDetailService timeSheetDetailService)
    {
        _timeSheetDetailService = timeSheetDetailService;
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
    public async Task<ValidationResult> AddTimeSheetDetailAsync([FromBody]TimeSheetDetail timeSheetDetail)
    {
        try
        {
            await _timeSheetDetailService.AddTimeSheetDetailAsync(timeSheetDetail);
        }
        catch (Exception ex)
        {
            validationResult.AddErrorMesageCode(UserActionConstants.TimeSheetAddFailed, UserActionConstants.ErrorDescriptions);
        }
        return validationResult;
    }

    /// <summary>
    /// Upadte method to Update TimeSheetDetail to database
    /// </summary>
    /// <param name="TimeSheetDetail">TimeSheetDetail object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPut]
    public async Task<ValidationResult> UpdateTimeSheetDetailAsync(TimeSheetDetail timeSheetDetail)
    {
        try
        {
            await _timeSheetDetailService.UpdateTimeSheetDetailAsync(timeSheetDetail);
        }
        catch (Exception ex)
        {
            if (timeSheetDetail.IsDeleted == true)
                validationResult.AddErrorMesageCode(UserActionConstants.TimeSheetDeleteFailed, UserActionConstants.ErrorDescriptions);
            else
                validationResult.AddErrorMesageCode(UserActionConstants.TimeSheetUpdateFailed, UserActionConstants.ErrorDescriptions);
        }
        return validationResult;
    }

    /// <summary>
    /// Delete method to delete TimeSheetDetail to database
    /// </summary>
    /// <param name="id">timeSheetDetail Id</param>
    /// <returns>returns bool</returns>
    [HttpDelete("{id:int}")]
    public async Task<ValidationResult> DeleteTimeSheetDetailAsync(int id)
    {
        try
        {
            await _timeSheetDetailService.DeleteTimeSheetDetailAsync(id);
        }
        catch (Exception ex)
        {
            validationResult.AddErrorMesageCode(UserActionConstants.TimeSheetDeleteFailed, UserActionConstants.ErrorDescriptions);
        }
        return validationResult;
    }
}
