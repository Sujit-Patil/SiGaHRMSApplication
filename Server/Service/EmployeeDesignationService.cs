using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Service;

public class EmployeeDesignationService : IEmployeeDesignationService
{
    private readonly IEmployeeDesignationRepository _employeeDesignationRepository;
    private ILogger<EmployeeDesignationService> _logger;

    /// <summary>
    /// Initializes a new instance 
    /// </summary>
    /// <param name="IEmployeeDesignationRepository">dfhgdj</param>
    /// <param name="ILogger<EmployeeDesignationService>">gfhk</param>
    public EmployeeDesignationService(IEmployeeDesignationRepository employeeDesignationRepository, ILogger<EmployeeDesignationService> logger)
    {
        _employeeDesignationRepository = employeeDesignationRepository;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task AddEmployeeDesignationAsync(EmployeeDesignation employeeDesignation)
    {

        await _employeeDesignationRepository.AddAsync(employeeDesignation);
        await _employeeDesignationRepository.CompleteAsync();
        _logger.LogInformation($"[AddEmployeeDesignationAsyns] - {employeeDesignation.EmployeeDesignationId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateEmployeeDesignationAsync(EmployeeDesignation employeeDesignation)
    {
        await _employeeDesignationRepository.UpdateAsync(employeeDesignation);
        await _employeeDesignationRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateEmployeeDesignationAsyns] - EmployeeDesignation updated successfully for the {employeeDesignation.EmployeeDesignationId}");
    }

    /// <inheritdoc/>
    public async Task<EmployeeDesignation?> GetEmployeeDesignationByIdAsync(int id)
    {
        return await _employeeDesignationRepository.
            FirstOrDefaultAsync(x => x.EmployeeDesignationId == id);
    }

    /// <inheritdoc/>
    public List<EmployeeDesignation> GetAllEmployeeDesignations()
    {
        var employeeDesignationList = _employeeDesignationRepository.GetAll();
        return (List<EmployeeDesignation>)employeeDesignationList;
    }

    /// <inheritdoc/>
    public async Task DeleteEmployeeDesignationAsync(int employeeDesignationId)
    {
        await _employeeDesignationRepository.DeleteAsync(x => x.EmployeeDesignationId == employeeDesignationId);
        await _employeeDesignationRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteEmployeeDesignationAsync] - EmployeeDesignation deleted successfully for the {employeeDesignationId}");
    }

}
