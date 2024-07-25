using Microsoft.EntityFrameworkCore;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Service;

public class IncentivePurposeService : IIncentivePurposeService
{
    private readonly IIncentivePurposeRepository _incentivePurposeRepository;
    private ILogger<IncentivePurposeService> _logger;

    /// <summary>
    /// Initializes a new instance 
    /// </summary>
    /// <param name="IIncentivePurposeRepository">dfhgdj</param>
    /// <param name="ILogger<IncentivePurposeService>">gfhk</param>
    public IncentivePurposeService(IIncentivePurposeRepository incentivePurposeRepository, ILogger<IncentivePurposeService> logger)
    {
        _incentivePurposeRepository = incentivePurposeRepository;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task AddIncentivePurposeAsync(IncentivePurpose incentivePurpose)
    {

        await _incentivePurposeRepository.AddAsync(incentivePurpose);
        await _incentivePurposeRepository.CompleteAsync();
        _logger.LogInformation($"[AddIncentivePurposeAsyns] - {incentivePurpose.IncentivePurposeId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateIncentivePurposeAsync(IncentivePurpose incentivePurpose)
    {
        await _incentivePurposeRepository.UpdateAsync(incentivePurpose);
        await _incentivePurposeRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateIncentivePurposeAsyns] - IncentivePurpose updated successfully for the {incentivePurpose.IncentivePurposeId}");
    }

    /// <inheritdoc/>
    public async Task<IncentivePurpose?> GetIncentivePurposeByIdAsync(int id)
    {
        return await _incentivePurposeRepository.
            FirstOrDefaultAsync(x => x.IncentivePurposeId == id);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<IncentivePurpose>> GetAllIncentivePurposes()
    {
        return await _incentivePurposeRepository.GetQueryable(x => x.IsDeleted == false).ToListAsync();

    }

    /// <inheritdoc/>
    public async Task DeleteIncentivePurposeAsync(int incentivePurposeId)
    {
        await _incentivePurposeRepository.DeleteAsync(x => x.IncentivePurposeId == incentivePurposeId);
        await _incentivePurposeRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteIncentivePurposeAsync] - IncentivePurpose deleted successfully for the {incentivePurposeId}");
    }

}
