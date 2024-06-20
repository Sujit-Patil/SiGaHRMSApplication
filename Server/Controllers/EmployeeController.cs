using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Controllers;

/// <summary>
/// Employee Controller 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    /// <summary>
    /// Initializes a new instance of see ref<paramref name="EmployeeController"/>
    /// </summary>
    /// <param name="employeeService"></param>
    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    /// <summary>
    /// The controller method to retrive all Employees.
    /// </summary>
    /// <returns>returns list of Employees</returns>
    
    [HttpGet]
    [Authorize(Roles ="Super Admin")]
    public List<Employee> GetAllEmployees()
    {
        return _employeeService.GetAllEmployees();
    }

    /// <summary>
    /// Get method to retrive single Employee
    /// </summary>
    /// <param name="id">Employee Id</param>
    /// <returns> return single Employee using Employee Id</returns>
    [HttpGet("{id:int}")]
    public async Task<Employee?> GetEmployeeByIdAsync(int id)
    {
        return await _employeeService.GetEmployeeByIdAsync(id);
    }

    /// <summary>
    /// Post method to Add Employee to database
    /// </summary>
    /// <param name="employee"> Employee object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPost]
    public async Task AddEmployeeAsync([FromBody] Employee employee)
    {
        await _employeeService.AddEmployeeAsync(employee);
    }

    /// <summary>
    /// Upadte method to Update Employee to database
    /// </summary>
    /// <param name="employee">Employee object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPut]
    public async Task UpdateEmployeeAsync(Employee employee)
    {
        await _employeeService.UpdateEmployeeAsync(employee);
    }

    /// <summary>
    /// Delete method to delete Employee to database
    /// </summary>
    /// <param name="id">Employee Id</param>
    /// <returns>returns bool</returns>
    [HttpDelete("{id:int}")]
    public async Task<bool> DeleteEmployeeAsync(int id)
    {
        await _employeeService.DeleteEmployeeAsync(id);
        return true;
    }
}
