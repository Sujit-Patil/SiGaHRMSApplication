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
/// Holiday Controller 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class HolidayController : ControllerBase
{
    private readonly IHolidayService _holidayService;
    private ValidationResult validationResult;
    private readonly ILogger<HolidayController> _logger;

    /// <summary>
    /// Initializes a new instance of see ref<paramref name="HolidayController"/>
    /// </summary>
    /// <param name="holidayService"></param>
    public HolidayController(IHolidayService holidayService, ILogger<HolidayController> logger)
    {
        _holidayService = holidayService;
        validationResult = new();
        _logger = logger;
    }

    /// <summary>
    /// The controller method to retrive all Holidays.
    /// </summary>
    /// <returns>returns list of Holidays</returns>

    [HttpGet]
    public Task<IEnumerable<Holiday>> GetAllHolidays()
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
    public async Task<IActionResult> AddHolidayAsync(Holiday holiday)
    {
        try
        {
            await _holidayService.AddHolidayAsync(holiday);
            return Ok(validationResult);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[AddHolidayAsync] Error Occurs : {ex.Message}");
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
    public List<Holiday> GetHolidaysByDateAsync(RequestDto holidayRequestDto)
    {
        return _holidayService.GetHolidaysByDateAsync(holidayRequestDto);
    }
    /// <summary>
    /// Upadte method to Update Holiday to database
    /// </summary>
    /// <param name="holiday">Holiday object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateHolidayAsync(Holiday holiday)
    {
        try
        {
            await _holidayService.UpdateHolidayAsync(holiday);
            return Ok(validationResult);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[AddHolidayAsync] Error Occurs : {ex.Message}");
            validationResult.AddErrorMesageCode(UserActionConstants.UnExpectedException, UserActionConstants.ErrorDescriptions);
            return BadRequest(validationResult);
        }
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
