using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Service;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private ILogger<EmployeeService> _logger;

    /// <summary>
    /// Initializes a new instance 
    /// </summary>
    /// <param name="IEmployeeRepository">dfhgdj</param>
    /// <param name="ILogger<EmployeeService>">gfhk</param>
    public EmployeeService(IEmployeeRepository employeeRepository, ILogger<EmployeeService> logger)
    {
        _employeeRepository = employeeRepository;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task AddEmployeeAsync(Employee employee)
    {

        await _employeeRepository.AddAsync(employee);
        await _employeeRepository.CompleteAsync();
        _logger.LogInformation($"[AddEmployeeAsyns] - {employee.EmployeeId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateEmployeeAsync(Employee employee)
    {
        await _employeeRepository.UpdateAsync(employee);
        await _employeeRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateEmployeeAsyns] - Employee updated successfully for the {employee.EmployeeId}");
    }

    /// <inheritdoc/>
    public async Task<Employee?> GetEmployeeByIdAsync(int id)
    {
        return await _employeeRepository.
            FirstOrDefaultAsync(x => x.EmployeeId == id);
    }

    /// <inheritdoc/>
    public List<Employee> GetAllEmployees()
    {
        var employeeList = _employeeRepository.GetAll();
        return (List<Employee>)employeeList;
    }

    /// <inheritdoc/>
    public async Task DeleteEmployeeAsync(int employeeId)
    {
        await _employeeRepository.DeleteAsync(x => x.EmployeeId == employeeId);
        await _employeeRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteEmployeeAsync] - Employee deleted successfully for the {employeeId}");
    }

}
