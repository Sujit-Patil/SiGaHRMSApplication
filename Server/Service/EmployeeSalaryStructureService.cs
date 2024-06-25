using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Service;

public class EmployeeSalaryStructureService : IEmployeeSalaryStructureService
{
    private readonly IEmployeeSalaryStructureRepository _employeeSalaryStructureRepository;
    private ILogger<EmployeeSalaryStructureService> _logger;

    /// <summary>
    /// Initializes a new instance 
    /// </summary>
    /// <param name="IEmployeeSalaryStructureRepository">dfhgdj</param>
    /// <param name="ILogger<EmployeeSalaryStructureService>">gfhk</param>
    public EmployeeSalaryStructureService(IEmployeeSalaryStructureRepository employeeSalaryStructureRepository, ILogger<EmployeeSalaryStructureService> logger)
    {
        _employeeSalaryStructureRepository = employeeSalaryStructureRepository;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task AddEmployeeSalaryStructureAsync(EmployeeSalaryStructure employeeSalaryStructure)
    {

        await _employeeSalaryStructureRepository.AddAsync(employeeSalaryStructure);
        await _employeeSalaryStructureRepository.CompleteAsync();
        _logger.LogInformation($"[AddEmployeeSalaryStructureAsyns] - {employeeSalaryStructure.EmployeeSalaryStructureId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateEmployeeSalaryStructureAsync(EmployeeSalaryStructure employeeSalaryStructure)
    {
        await _employeeSalaryStructureRepository.UpdateAsync(employeeSalaryStructure);
        await _employeeSalaryStructureRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateEmployeeSalaryStructureAsyns] - EmployeeSalaryStructure updated successfully for the {employeeSalaryStructure.EmployeeSalaryStructureId}");
    }

    /// <inheritdoc/>
    public async Task<EmployeeSalaryStructure?> GetEmployeeSalaryStructureByIdAsync(int id)
    {
        return await _employeeSalaryStructureRepository.
            FirstOrDefaultAsync(x => x.EmployeeSalaryStructureId == id);
    }

    /// <inheritdoc/>
    public List<EmployeeSalaryStructure> GetAllEmployeeSalaryStructures()
    {
        var employeeSalaryStructureList = _employeeSalaryStructureRepository.GetAll();
        return (List<EmployeeSalaryStructure>)employeeSalaryStructureList;
    }

    /// <inheritdoc/>
    public async Task DeleteEmployeeSalaryStructureAsync(int employeeSalaryStructureId)
    {
        await _employeeSalaryStructureRepository.DeleteAsync(x => x.EmployeeSalaryStructureId == employeeSalaryStructureId);
        await _employeeSalaryStructureRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteEmployeeSalaryStructureAsync] - EmployeeSalaryStructure deleted successfully for the {employeeSalaryStructureId}");
    }

}
