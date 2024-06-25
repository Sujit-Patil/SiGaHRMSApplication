using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Constants;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Controllers;

/// <summary>TimeSheetDetailController
/// TimeSheetDetail Controller 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TimeSheetDetailController : ControllerBase
{
    private readonly ITimeSheetDetailService _timeSheetDetailService;

    /// <summary>
    /// Initializes a new instance of see ref<paramref name="timeSheetDetailController"/>
    /// </summary>
    /// <param name="TimeSheetDetailService"></param>
    public TimeSheetDetailController(ITimeSheetDetailService timeSheetDetailService)
    {
        _timeSheetDetailService = timeSheetDetailService;
    }

    /// <summary>
    /// The controller method to retrive all TimeSheetDetails.
    /// </summary>
    /// <returns>returns list of TimeSheetDetails</returns>
    
    [HttpGet]
    [Authorize(Roles =RoleConstants.SUPERADMIN)]
    public List<TimeSheetDetail> GetAllTimeSheetDetails()
    {
        return _timeSheetDetailService.GetAllTimeSheetDetails();
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
    public async Task AddTimeSheetDetailAsync(TimeSheetDetail timeSheetDetail)
    {
        await _timeSheetDetailService.AddTimeSheetDetailAsync(timeSheetDetail);
    }

    /// <summary>
    /// Upadte method to Update TimeSheetDetail to database
    /// </summary>
    /// <param name="TimeSheetDetail">TimeSheetDetail object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPut]
    public async Task UpdateTimeSheetDetailAsync(TimeSheetDetail timeSheetDetail)
    {
        await _timeSheetDetailService.UpdateTimeSheetDetailAsync(timeSheetDetail);
    }

    /// <summary>
    /// Delete method to delete TimeSheetDetail to database
    /// </summary>
    /// <param name="id">timeSheetDetail Id</param>
    /// <returns>returns bool</returns>
    [HttpDelete("{id:int}")]
    public async Task<bool> DeleteTimeSheetDetailAsync(int id)
    {
        await _timeSheetDetailService.DeleteTimeSheetDetailAsync(id);
        return true;
    }
}
