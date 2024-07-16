using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Service;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    private ILogger<DepartmentService> _logger;

    /// <summary>
    /// Initializes a new instance of the DepartmentService class.
    /// </summary>
    /// <param name="departmentRepository">The repository for managing department data.</param>
    /// <param name="logger">The logger for logging messages related to DepartmentService.</param>
    public DepartmentService(
        IDepartmentRepository departmentRepository,
        ILogger<DepartmentService> logger)
    {
        _departmentRepository = departmentRepository;
        _logger = logger;
    }


    /// <inheritdoc/>
    public async Task AddDepartmentAsync(Department department)
    {

        await _departmentRepository.AddAsync(department);
        await _departmentRepository.CompleteAsync();
        _logger.LogInformation($"[AddDepartmentAsyns] - {department.DepartmentId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateDepartmentAsync(Department department)
    {
        await _departmentRepository.UpdateAsync(department);
        await _departmentRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateDepartmentAsyns] - Department updated successfully for the {department.DepartmentId}");
    }

    /// <inheritdoc/>
    public async Task<Department?> GetDepartmentByIdAsync(int id)
    {
        return await _departmentRepository.
            FirstOrDefaultAsync(x => x.DepartmentId == id);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<Department>> GetAllDepartments()
    {
        return _departmentRepository.GetAllAsync();
    }

    /// <inheritdoc/>
    public async Task DeleteDepartmentAsync(int departmentId)
    {
        await _departmentRepository.DeleteAsync(x => x.DepartmentId == departmentId);
        await _departmentRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteDepartmentAsync] - Department deleted successfully for the {departmentId}");
    }

}
