using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Service;

public class DesignationService : IDesignationService
{
    private readonly IDesignationRepository _designationRepository;
    private ILogger<DesignationService> _logger;

    /// <summary>
    /// Initializes a new instance of the DesignationService class.
    /// </summary>
    /// <param name="designationRepository">The repository for managing designation data.</param>
    /// <param name="logger">The logger for logging messages related to DesignationService.</param>
    public DesignationService(
        IDesignationRepository designationRepository,
        ILogger<DesignationService> logger)
    {
        _designationRepository = designationRepository;
        _logger = logger;
    }


    /// <inheritdoc/>
    public async Task AddDesignationAsync(Designation designation)
    {

        await _designationRepository.AddAsync(designation);
        await _designationRepository.CompleteAsync();
        _logger.LogInformation($"[AddDesignationAsyns] - {designation.DesignationId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateDesignationAsync(Designation designation)
    {
        await _designationRepository.UpdateAsync(designation);
        await _designationRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateDesignationAsyns] - Designation updated successfully for the {designation.DesignationId}");
    }

    /// <inheritdoc/>
    public async Task<Designation?> GetDesignationByIdAsync(int id)
    {
        return await _designationRepository.
            FirstOrDefaultAsync(x => x.DesignationId == id);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<Designation>> GetAllDesignations()
    {
        return _designationRepository.GetAllAsync();
    }

    /// <inheritdoc/>
    public async Task DeleteDesignationAsync(int designationId)
    {
        await _designationRepository.DeleteAsync(x => x.DesignationId == designationId);
        await _designationRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteDesignationAsync] - Designation deleted successfully for the {designationId}");
    }

}
