using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Interfaces;

/// <summary>
/// Employee service interface to perform Add, Update, and delete funtionality.
/// </summary>
/// <param name="Employee"></param>
public interface IEmployeeService
{
    /// <summary>
    /// AddEmployeeAsync method will add Employee to Db
    /// </summary>
    /// <param name="employee">Employee</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task AddEmployeeAsync(Employee employee);

    /// <summary>
    /// UpdateEmployeeAsync method perform update funtionality
    /// </summary>
    /// <param name="employee">Employee</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task UpdateEmployeeAsync(Employee employee);

    /// <summary>
    /// DeleteEmployeeAsync method perform Delete funtionality
    /// </summary>
    /// <param name="employeeid">Employee Id</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task DeleteEmployeeAsync(int employeeId);

    /// <summary>
    /// GetEmployeeByIdAsync method gives Employee using Id
    /// </summary>
    /// <param name="id">Employee Id</param>
    /// <returns>Returns single Employee </returns>
    public Task<Employee?> GetEmployeeByIdAsync(int id);

    /// <summary>
    /// GetAllEmployees gives list of Employees
    /// </summary>
    /// <returns>Returns list of Employee</returns>
    public List<Employee> GetAllEmployees();

    /// <summary>
    /// GetEmployeeByIdAsync method gives Employee using email
    /// </summary>
    /// <param name="email">Employee Email</param>
    /// <returns>Returns single Employee </returns>
    public Task<Employee?> GetEmployeeByEmailAsync(string email);

}
