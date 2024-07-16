using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Dto;

namespace SiGaHRMS.ApiService.Interfaces;

/// <summary>
/// EmployeeSalaryStructure service interface to perform Add, Update, and delete funtionality.
/// </summary>
/// <param name="EmployeeSalaryStructure"></param>
public interface IEmployeeSalaryStructureService
{
    /// <summary>
    /// AddEmployeeSalaryStructureAsync method will add EmployeeSalaryStructure to Db
    /// </summary>
    /// <param name="employeeSalaryStructure">EmployeeSalaryStructure</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task AddEmployeeSalaryStructureAsync(EmployeeSalaryStructure employeeSalaryStructure);

    /// <summary>
    /// UpdateEmployeeSalaryStructureAsync method perform update funtionality
    /// </summary>
    /// <param name="employeeSalaryStructure">EmployeeSalaryStructure</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task UpdateEmployeeSalaryStructureAsync(EmployeeSalaryStructure employeeSalaryStructure);

    /// <summary>
    /// DeleteEmployeeSalaryStructureAsync method perform Delete funtionality
    /// </summary>
    /// <param name="employeeSalaryStructureid">EmployeeSalaryStructure Id</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task DeleteEmployeeSalaryStructureAsync(int employeeSalaryStructureId);

    /// <summary>
    /// GetEmployeeSalaryStructureByIdAsync method gives EmployeeSalaryStructure using Id
    /// </summary>
    /// <param name="id">EmployeeSalaryStructure Id</param>
    /// <returns>Returns single EmployeeSalaryStructure </returns>
    public Task<EmployeeSalaryStructure?> GetEmployeeSalaryStructureByIdAsync(int id);

    /// <summary>
    /// GetAllEmployeeSalaryStructures gives list of EmployeeSalaryStructures
    /// </summary>
    /// <returns>Returns list of EmployeeSalaryStructure</returns>
    public Task<IEnumerable<EmployeeSalaryStructure>> GetAllEmployeeSalaryStructures();

    /// <summary>
    /// Get Employees Salary Structures list according to RequestDto
    /// </summary>
    /// <param name="employeeSalaryStructureDto">requestDto Id</param>
    /// <returns>Returns list of EmployeeSalaryStructure</returns>
    public List<EmployeeSalaryStructure> GetEmployeeSalaryStructuresByDateAsync(RequestDto requestDto);

}
