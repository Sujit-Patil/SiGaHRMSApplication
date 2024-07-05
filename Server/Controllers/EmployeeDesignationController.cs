using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Constants;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Controllers;

/// <summary>
/// EmployeeDesignation Controller 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class EmployeeDesignationController : ControllerBase
{
    private readonly IEmployeeDesignationService _employeeDesignationService;

    /// <summary>
    /// Initializes a new instance of see ref<paramref name="EmployeeDesignationController"/>
    /// </summary>
    /// <param name="employeeDesignationService"></param>
    public EmployeeDesignationController(IEmployeeDesignationService employeeDesignationService)
    {
        _employeeDesignationService = employeeDesignationService;
    }

    /// <summary>
    /// The controller method to retrive all EmployeeDesignations.
    /// </summary>
    /// <returns>returns list of EmployeeDesignations</returns>
    
    [HttpGet]
    [Authorize(Roles =RoleConstants.SUPERADMIN)]
    public Task<IEnumerable<EmployeeDesignation>> GetAllEmployeeDesignations()
    {
        return _employeeDesignationService.GetAllEmployeeDesignations();
    }

    /// <summary>
    /// Get method to retrive single EmployeeDesignation
    /// </summary>
    /// <param name="id">EmployeeDesignation Id</param>
    /// <returns> return single EmployeeDesignation using EmployeeDesignation Id</returns>
    [HttpGet("{id:int}")]
    public async Task<EmployeeDesignation?> GetEmployeeDesignationByIdAsync(int id)
    {
        return await _employeeDesignationService.GetEmployeeDesignationByIdAsync(id);
    }

    /// <summary>
    /// Post method to Add EmployeeDesignation to database
    /// </summary>
    /// <param name="employeeDesignation"> EmployeeDesignation object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPost]
    public async Task AddEmployeeDesignationAsync(EmployeeDesignation employeeDesignation)
    {
        await _employeeDesignationService.AddEmployeeDesignationAsync(employeeDesignation);
    }

    /// <summary>
    /// Upadte method to Update EmployeeDesignation to database
    /// </summary>
    /// <param name="employeeDesignation">EmployeeDesignation object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPut]
    public async Task UpdateEmployeeDesignationAsync(EmployeeDesignation employeeDesignation)
    {
        await _employeeDesignationService.UpdateEmployeeDesignationAsync(employeeDesignation);
    }

    /// <summary>
    /// Delete method to delete EmployeeDesignation to database
    /// </summary>
    /// <param name="id">EmployeeDesignation Id</param>
    /// <returns>returns bool</returns>
    [HttpDelete("{id:int}")]
    public async Task<bool> DeleteEmployeeDesignationAsync(int id)
    {
        await _employeeDesignationService.DeleteEmployeeDesignationAsync(id);
        return true;
    }
}
