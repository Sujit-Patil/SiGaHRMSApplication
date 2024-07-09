using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.ApiService.Service;
using SiGaHRMS.Data.Constants;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Dto;

namespace SiGaHRMS.ApiService.Controllers;

/// <summary>EmployeeSalaryStructureController
/// EmployeeSalaryStructure Controller 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class EmployeeSalaryStructureController : ControllerBase
{
    private readonly IEmployeeSalaryStructureService _employeeSalaryStructureService;

    /// <summary>
    /// Initializes a new instance of see ref<paramref name="EmployeeSalaryStructureController"/>
    /// </summary>
    /// <param name="employeeSalaryStructureService"></param>
    public EmployeeSalaryStructureController(IEmployeeSalaryStructureService employeeSalaryStructureService)
    {
        _employeeSalaryStructureService = employeeSalaryStructureService;
    }

    /// <summary>
    /// The controller method to retrive all EmployeeSalaryStructures.
    /// </summary>
    /// <returns>returns list of EmployeeSalaryStructures</returns>
    
    [HttpGet]
    [Authorize(Roles =RoleConstants.SUPERADMIN)]
    public Task<IEnumerable<EmployeeSalaryStructure>> GetAllEmployeeSalaryStructures()
    {
        return _employeeSalaryStructureService.GetAllEmployeeSalaryStructures();
    }

    /// <summary>
    /// Get method to retrive Attendance By Date
    /// </summary>
    /// <param name="AttendanceDto">attendanceDto</param>
    /// <returns> return List of Attendance using Date</returns>
    [HttpPost("ByDate")]
    public List<EmployeeSalaryStructure> GetEmployeeSalaryStructuresByDateAsync(RequestDto employeeSalaryStructureDto)
    {
        return _employeeSalaryStructureService.GetEmployeeSalaryStructuresByDateAsync(employeeSalaryStructureDto);
    }

    /// <summary>
    /// Get method to retrive single EmployeeSalaryStructure
    /// </summary>
    /// <param name="id">EmployeeSalaryStructure Id</param>
    /// <returns> return single EmployeeSalaryStructure using EmployeeSalaryStructure Id</returns>
    [HttpGet("{id:int}")]
    public async Task<EmployeeSalaryStructure?> GetEmployeeSalaryStructureByIdAsync(int id)
    {
        return await _employeeSalaryStructureService.GetEmployeeSalaryStructureByIdAsync(id);
    }

    /// <summary>
    /// Post method to Add EmployeeSalaryStructure to database
    /// </summary>
    /// <param name="employeeSalaryStructure"> EmployeeSalaryStructure object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPost]
    public async Task AddEmployeeSalaryStructureAsync(EmployeeSalaryStructure employeeSalaryStructure)
    {
        await _employeeSalaryStructureService.AddEmployeeSalaryStructureAsync(employeeSalaryStructure);
    }

    /// <summary>
    /// Upadte method to Update EmployeeSalaryStructure to database
    /// </summary>
    /// <param name="employeeSalaryStructure">EmployeeSalaryStructure object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPut]
    public async Task UpdateEmployeeSalaryStructureAsync(EmployeeSalaryStructure employeeSalaryStructure)
    {
        await _employeeSalaryStructureService.UpdateEmployeeSalaryStructureAsync(employeeSalaryStructure);
    }

    /// <summary>
    /// Delete method to delete EmployeeSalaryStructure to database
    /// </summary>
    /// <param name="id">EmployeeSalaryStructure Id</param>
    /// <returns>returns bool</returns>
    [HttpDelete("{id:int}")]
    public async Task<bool> DeleteEmployeeSalaryStructureAsync(int id)
    {
        await _employeeSalaryStructureService.DeleteEmployeeSalaryStructureAsync(id);
        return true;
    }
}
