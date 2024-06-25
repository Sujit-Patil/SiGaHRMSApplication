using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Constants;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Controllers;

/// <summary>
/// Holiday Controller 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class HolidayController : ControllerBase
{
    private readonly IHolidayService _holidayService;

    /// <summary>
    /// Initializes a new instance of see ref<paramref name="HolidayController"/>
    /// </summary>
    /// <param name="holidayService"></param>
    public HolidayController(IHolidayService holidayService)
    {
        _holidayService = holidayService;
    }

    /// <summary>
    /// The controller method to retrive all Holidays.
    /// </summary>
    /// <returns>returns list of Holidays</returns>
    
    [HttpGet]
    [Authorize(Roles =RoleConstants.SUPERADMIN)]
    public List<Holiday> GetAllHolidays()
    {
        return _holidayService.GetAllHolidays();
    }

    /// <summary>
    /// Get method to retrive single Holiday
    /// </summary>
    /// <param name="id">Holiday Id</param>
    /// <returns> return single Holiday using Holiday Id</returns>
    [HttpGet("{id:int}")]
    public async Task<Holiday?> GetHolidayByIdAsync(int id)
    {
        return await _holidayService.GetHolidayByIdAsync(id);
    }

    /// <summary>
    /// Post method to Add Holiday to database
    /// </summary>
    /// <param name="holiday"> Holiday object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPost]
    public async Task AddHolidayAsync(Holiday holiday)
    {
        await _holidayService.AddHolidayAsync(holiday);
    }

    /// <summary>
    /// Upadte method to Update Holiday to database
    /// </summary>
    /// <param name="holiday">Holiday object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPut]
    public async Task UpdateHolidayAsync(Holiday holiday)
    {
        await _holidayService.UpdateHolidayAsync(holiday);
    }

    /// <summary>
    /// Delete method to delete Holiday to database
    /// </summary>
    /// <param name="id">Holiday Id</param>
    /// <returns>returns bool</returns>
    [HttpDelete("{id:int}")]
    public async Task<bool> DeleteHolidayAsync(int id)
    {
        await _holidayService.DeleteHolidayAsync(id);
        return true;
    }
}
