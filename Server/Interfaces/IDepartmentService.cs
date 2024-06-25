using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Interfaces;

/// <summary>
/// Department service interface to perform Add, Update, and delete funtionality.
/// </summary>
/// <param name="Department"></param>
public interface IDepartmentService
{
    /// <summary>
    /// AddDepartmentAsync method will add Department to Db
    /// </summary>
    /// <param name="department">Department</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task AddDepartmentAsync(Department department);

    /// <summary>
    /// UpdateDepartmentAsync method perform update funtionality
    /// </summary>
    /// <param name="department">Department</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task UpdateDepartmentAsync(Department department);

    /// <summary>
    /// DeleteDepartmentAsync method perform Delete funtionality
    /// </summary>
    /// <param name="departmentid">Department Id</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task DeleteDepartmentAsync(int departmentId);

    /// <summary>
    /// GetDepartmentByIdAsync method gives Department using Id
    /// </summary>
    /// <param name="id">Department Id</param>
    /// <returns>Returns single Department </returns>
    public Task<Department?> GetDepartmentByIdAsync(int id);

    /// <summary>
    /// GetAllDepartments gives list of Departments
    /// </summary>
    /// <returns>Returns list of Department</returns>
    public List<Department> GetAllDepartments();

}
