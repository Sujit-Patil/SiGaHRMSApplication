using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.ApiService.Service;
using SiGaHRMS.Data.Constants;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Dto;

namespace SiGaHRMS.ApiService.Controllers;

/// <summary>TimeSheetController
/// TimeSheet Controller 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TimeSheetController : ControllerBase
{
    private readonly ITimesheetService _timeSheetService;

    /// <summary>
    /// Initializes a new instance of see ref<paramref name="timeSheetController"/>
    /// </summary>
    /// <param name="TimeSheetService"></param>
    public TimeSheetController(ITimesheetService timeSheetService)
    {
        _timeSheetService = timeSheetService;
    }

    /// <summary>
    /// The controller method to retrive all TimeSheets.
    /// </summary>
    /// <returns>returns list of TimeSheets</returns>
    
    [HttpGet]
    [Authorize(Roles =RoleConstants.SUPERADMIN)]
    public Task<IEnumerable<Timesheet>> GetAllTimeSheets()
    {
        return _timeSheetService.GetAllTimesheets();
    }


    /// <summary>
    /// Get method to retrive Attendance By Date
    /// </summary>
    /// <param name="AttendanceDto">attendanceDto</param>
    /// <returns> return List of Attendance using Date</returns>
    [HttpPost("ByDate")]
    public List<Timesheet> GetTimeSheetByDateAsync(RequestDto timesheetDto)
    {
        return _timeSheetService.GetTimesheetsByDateAsync(timesheetDto);
    }

    /// <summary>
    /// Get method to retrive single TimeSheet
    /// </summary>
    /// <param name="id">timeSheet Id</param>
    /// <returns> return single TimeSheet using timeSheet Id</returns>
    [HttpGet("{id:int}")]
    public async Task<Timesheet?> GetTimeSheetByIdAsync(int id)
    {
        return await _timeSheetService.GetTimesheetByIdAsync(id);
    }

    /// <summary>
    /// Post method to Add TimeSheet to database
    /// </summary>
    /// <param name="TimeSheet"> TimeSheet object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPost]
    public async Task AddTimeSheetAsync(Timesheet timeSheet)
    {
        await _timeSheetService.AddTimesheetAsync(timeSheet);
    }

    /// <summary>
    /// Upadte method to Update TimeSheet to database
    /// </summary>
    /// <param name="TimeSheet">TimeSheet object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPut]
    public async Task UpdateTimeSheetAsync(Timesheet timeSheet)
    {
        await _timeSheetService.UpdateTimesheetAsync(timeSheet);
    }

    /// <summary>
    /// Delete method to delete TimeSheet to database
    /// </summary>
    /// <param name="id">timeSheet Id</param>
    /// <returns>returns bool</returns>
    [HttpDelete("{id:int}")]
    public async Task<bool> DeleteTimeSheetAsync(int id)
    {
        await _timeSheetService.DeleteTimesheetAsync(id);
        return true;
    }
}
