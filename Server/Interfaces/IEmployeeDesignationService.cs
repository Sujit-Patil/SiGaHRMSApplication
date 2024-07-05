using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Interfaces;

/// <summary>
/// EmployeeDesignation service interface to perform Add, Update, and delete funtionality.
/// </summary>
/// <param name="EmployeeDesignation"></param>
public interface IEmployeeDesignationService
{
    /// <summary>
    /// AddEmployeeDesignationAsync method will add EmployeeDesignation to Db
    /// </summary>
    /// <param name="employeeDesignation">EmployeeDesignation</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task AddEmployeeDesignationAsync(EmployeeDesignation employeeDesignation);

    /// <summary>
    /// UpdateEmployeeDesignationAsync method perform update funtionality
    /// </summary>
    /// <param name="employeeDesignation">EmployeeDesignation</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task UpdateEmployeeDesignationAsync(EmployeeDesignation employeeDesignation);

    /// <summary>
    /// DeleteEmployeeDesignationAsync method perform Delete funtionality
    /// </summary>
    /// <param name="employeeDesignationid">EmployeeDesignation Id</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task DeleteEmployeeDesignationAsync(int employeeDesignationId);

    /// <summary>
    /// GetEmployeeDesignationByIdAsync method gives EmployeeDesignation using Id
    /// </summary>
    /// <param name="id">EmployeeDesignation Id</param>
    /// <returns>Returns single EmployeeDesignation </returns>
    public Task<EmployeeDesignation?> GetEmployeeDesignationByIdAsync(int id);

    /// <summary>
    /// GetAllEmployeeDesignations gives list of EmployeeDesignations
    /// </summary>
    /// <returns>Returns list of EmployeeDesignation</returns>
    public Task<IEnumerable<EmployeeDesignation>> GetAllEmployeeDesignations();

}
