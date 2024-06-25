using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Interfaces;

/// <summary>
/// EmployeeSalary service interface to perform Add, Update, and delete funtionality.
/// </summary>
/// <param name="EmployeeSalary"></param>
public interface IEmployeeSalaryService
{
    /// <summary>
    /// AddEmployeeSalaryAsync method will add EmployeeSalary to Db
    /// </summary>
    /// <param name="employeeSalary">EmployeeSalary</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task AddEmployeeSalaryAsync(EmployeeSalary employeeSalary);

    /// <summary>
    /// UpdateEmployeeSalaryAsync method perform update funtionality
    /// </summary>
    /// <param name="employeeSalary">EmployeeSalary</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task UpdateEmployeeSalaryAsync(EmployeeSalary employeeSalary);

    /// <summary>
    /// DeleteEmployeeSalaryAsync method perform Delete funtionality
    /// </summary>
    /// <param name="employeeSalaryid">EmployeeSalary Id</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task DeleteEmployeeSalaryAsync(int employeeSalaryId);

    /// <summary>
    /// GetEmployeeSalaryByIdAsync method gives EmployeeSalary using Id
    /// </summary>
    /// <param name="id">EmployeeSalary Id</param>
    /// <returns>Returns single EmployeeSalary </returns>
    public Task<EmployeeSalary?> GetEmployeeSalaryByIdAsync(int id);

    /// <summary>
    /// GetAllEmployeeSalarys gives list of EmployeeSalarys
    /// </summary>
    /// <returns>Returns list of EmployeeSalary</returns>
    public List<EmployeeSalary> GetAllEmployeeSalarys();

}
