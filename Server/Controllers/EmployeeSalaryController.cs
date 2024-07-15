using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Constants;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Dto;
using SiGaHRMS.Data.Validations;

namespace SiGaHRMS.ApiService.Controllers;

/// <summary>
/// EmployeeSalary Controller 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class EmployeeSalaryController : ControllerBase
{
    private readonly IEmployeeSalaryService _employeeSalaryService;
    private readonly ILogger<EmployeeSalaryController> _logger;
    private ValidationResult validationResult;

    /// <summary>
    /// Initializes a new instance of see ref<paramref name="EmployeeSalaryController"/>
    /// </summary>
    /// <param name="employeeSalaryService"></param>
    public EmployeeSalaryController(IEmployeeSalaryService employeeSalaryService, ILogger<EmployeeSalaryController> logger)
    {
        _employeeSalaryService = employeeSalaryService;
        _logger = logger;
        validationResult = new ();
    }

    /// <summary>
    /// The controller method to retrive all EmployeeSalarys.
    /// </summary>
    /// <returns>returns list of EmployeeSalarys</returns>
    
    [HttpGet]
    [Authorize(Roles =RoleConstants.SUPERADMIN)]
    public Task<IEnumerable<EmployeeSalary>> GetAllEmployeeSalarys()
    {
        return _employeeSalaryService.GetAllEmployeeSalarys();
    }

    /// <summary>
    /// Get method to retrive single EmployeeSalary
    /// </summary>
    /// <param name="id">EmployeeSalary Id</param>
    /// <returns> return single EmployeeSalary using EmployeeSalary Id</returns>
    [HttpGet("{id:int}")]
    public async Task<EmployeeSalary?> GetEmployeeSalaryByIdAsync(int id)
    {
        return await _employeeSalaryService.GetEmployeeSalaryByIdAsync(id);
    }

    /// <summary>
    /// Post method to Add EmployeeSalary to database
    /// </summary>
    /// <param name="employeeSalary"> EmployeeSalary object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPost]
    [Authorize(Roles = RoleConstants.SUPERADMIN + "," + RoleConstants.HR)]
    public async Task<IActionResult> AddEmployeeSalaryAsync(EmployeeSalary employeeSalary)
    {
        try
        {
            await _employeeSalaryService.AddEmployeeSalaryAsync(employeeSalary);
            return Ok(validationResult);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[AddEmployeeSalaryAsync] Error Occurs : {ex.Message}");
            validationResult.AddErrorMesageCode(UserActionConstants.UnExpectedException, UserActionConstants.ErrorDescriptions);
            return BadRequest(validationResult);
        }
    }

    /// <summary>
    /// Get method to retrive Attendance By Date
    /// </summary>
    /// <param name="AttendanceDto">attendanceDto</param>
    /// <returns> return List of Attendance using Date</returns>
    [HttpPost("ByDate")]
    public List<EmployeeSalary> GetEmployeeSalaryByDateAsync(RequestDto employeeSalaryDto)
    {
        return _employeeSalaryService.GetEmployeeSalaryByDateAsync(employeeSalaryDto);
    }

    /// <summary>
    /// Upadte method to Update EmployeeSalary to database
    /// </summary>
    /// <param name="employeeSalary">EmployeeSalary object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPut]
    [Authorize(Roles = RoleConstants.SUPERADMIN + "," + RoleConstants.HR)]
    public async Task UpdateEmployeeSalaryAsync(EmployeeSalary employeeSalary)
    {
        await _employeeSalaryService.UpdateEmployeeSalaryAsync(employeeSalary);
    }

    /// <summary>
    /// Delete method to delete EmployeeSalary to database
    /// </summary>
    /// <param name="id">EmployeeSalary Id</param>
    /// <returns>returns bool</returns>
    [HttpDelete("{id:int}")]
    public async Task<bool> DeleteEmployeeSalaryAsync(int id)
    {
        await _employeeSalaryService.DeleteEmployeeSalaryAsync(id);
        return true;
    }
}
