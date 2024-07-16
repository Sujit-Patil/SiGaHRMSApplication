using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Service;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IImageService _iImageService;
    private readonly IAuditingService _auditingService;
    private ILogger<EmployeeService> _logger;

    //// <summary>
    /// Initializes a new instance of the EmployeeService class.
    /// </summary>
    /// <param name="employeeRepository">The repository for managing employee data.</param>
    /// <param name="logger">The logger for logging messages related to EmployeeService.</param>
    /// <param name="iImageService">The service for managing images related to employees.</param>
    /// <param name="auditingService">The service for auditing operations.</param>
    public EmployeeService(
        IEmployeeRepository employeeRepository,
        ILogger<EmployeeService> logger,
        IImageService iImageService,
        IAuditingService auditingService)
    {
        _employeeRepository = employeeRepository;
        _logger = logger;
        _iImageService = iImageService;
        _auditingService = auditingService;
    }


    /// <inheritdoc/>
    public async Task AddEmployeeAsync(Employee employee)
    {
        employee = _auditingService.SetAuditedEntity(employee, created: true);
        await _employeeRepository.AddAsync(employee);
        await _employeeRepository.CompleteAsync();
        _logger.LogInformation($"[AddEmployeeAsyns] - Employee {employee.EmployeeId} added successfully");
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
    public async Task<Employee?> GetEmployeeByEmailAsync(string email)
    {
        return await _employeeRepository.
            FirstOrDefaultAsync(x => x.CompanyEmail == email);
    }

    /// <inheritdoc/>
    public List<Employee> GetAllEmployees()
    {
        return _employeeRepository.GetQueryable(filter: x => x.IsDeleted == false).ToList();
    }

    /// <inheritdoc/>
    public async Task DeleteEmployeeAsync(int employeeId)
    {
        await _employeeRepository.DeleteAsync(x => x.EmployeeId == employeeId);
        await _employeeRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteEmployeeAsync] - Employee deleted successfully for the {employeeId}");
    }

}
