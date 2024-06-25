using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Service;

public class EmployeeSalaryService : IEmployeeSalaryService
{
    private readonly IEmployeeSalaryRepository _employeeSalaryRepository;
    private ILogger<EmployeeSalaryService> _logger;

    /// <summary>
    /// Initializes a new instance 
    /// </summary>
    /// <param name="IEmployeeSalaryRepository">dfhgdj</param>
    /// <param name="ILogger<EmployeeSalaryService>">gfhk</param>
    public EmployeeSalaryService(IEmployeeSalaryRepository employeeSalaryRepository, ILogger<EmployeeSalaryService> logger)
    {
        _employeeSalaryRepository = employeeSalaryRepository;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task AddEmployeeSalaryAsync(EmployeeSalary employeeSalary)
    {

        await _employeeSalaryRepository.AddAsync(employeeSalary);
        await _employeeSalaryRepository.CompleteAsync();
        _logger.LogInformation($"[AddEmployeeSalaryAsyns] - {employeeSalary.EmployeeSalaryId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateEmployeeSalaryAsync(EmployeeSalary employeeSalary)
    {
        await _employeeSalaryRepository.UpdateAsync(employeeSalary);
        await _employeeSalaryRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateEmployeeSalaryAsyns] - EmployeeSalary updated successfully for the {employeeSalary.EmployeeSalaryId}");
    }

    /// <inheritdoc/>
    public async Task<EmployeeSalary?> GetEmployeeSalaryByIdAsync(int id)
    {
        return await _employeeSalaryRepository.
            FirstOrDefaultAsync(x => x.EmployeeSalaryId == id);
    }

    /// <inheritdoc/>
    public List<EmployeeSalary> GetAllEmployeeSalarys()
    {
        var employeeSalaryList = _employeeSalaryRepository.GetAll();
        return (List<EmployeeSalary>)employeeSalaryList;
    }

    /// <inheritdoc/>
    public async Task DeleteEmployeeSalaryAsync(int employeeSalaryId)
    {
        await _employeeSalaryRepository.DeleteAsync(x => x.EmployeeSalaryId == employeeSalaryId);
        await _employeeSalaryRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteEmployeeSalaryAsync] - EmployeeSalary deleted successfully for the {employeeSalaryId}");
    }

}
