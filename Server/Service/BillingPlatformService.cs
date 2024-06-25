using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Service;

public class BillingPlatformService : IBillingPlatformService
{
    private readonly IBillingPlatformRepository _billingPlatformRepository;
    private ILogger<BillingPlatformService> _logger;

    /// <summary>
    /// Initializes a new instance 
    /// </summary>
    /// <param name="IBillingPlatformRepository">dfhgdj</param>
    /// <param name="ILogger<BillingPlatformService>">gfhk</param>
    public BillingPlatformService(IBillingPlatformRepository billingPlatformRepository, ILogger<BillingPlatformService> logger)
    {
        _billingPlatformRepository = billingPlatformRepository;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task AddBillingPlatformAsync(BillingPlatform billingPlatform)
    {

        await _billingPlatformRepository.AddAsync(billingPlatform);
        await _billingPlatformRepository.CompleteAsync();
        _logger.LogInformation($"[AddBillingPlatformAsyns] - {billingPlatform.BillingPlatformId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateBillingPlatformAsync(BillingPlatform billingPlatform)
    {
        await _billingPlatformRepository.UpdateAsync(billingPlatform);
        await _billingPlatformRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateBillingPlatformAsyns] - BillingPlatform updated successfully for the {billingPlatform.BillingPlatformId}");
    }

    /// <inheritdoc/>
    public async Task<BillingPlatform?> GetBillingPlatformByIdAsync(int id)
    {
        return await _billingPlatformRepository.
            FirstOrDefaultAsync(x => x.BillingPlatformId == id);
    }

    /// <inheritdoc/>
    public List<BillingPlatform> GetAllBillingPlatforms()
    {
        var billingPlatformList = _billingPlatformRepository.GetAll();
        return (List<BillingPlatform>)billingPlatformList;
    }

    /// <inheritdoc/>
    public async Task DeleteBillingPlatformAsync(int billingPlatformId)
    {
        await _billingPlatformRepository.DeleteAsync(x => x.BillingPlatformId == billingPlatformId);
        await _billingPlatformRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteBillingPlatformAsync] - BillingPlatform deleted successfully for the {billingPlatformId}");
    }

}
