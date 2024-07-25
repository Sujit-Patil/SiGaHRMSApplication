using Microsoft.EntityFrameworkCore;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Service;

public class BillingPlatformService : IBillingPlatformService
{
    private readonly IBillingPlatformRepository _billingPlatformRepository;
    private readonly IAuditingService _auditingService;
    private ILogger<BillingPlatformService> _logger;

    /// <summary>
    /// Initializes a new instance of the BillingPlatformService class.
    /// </summary>
    /// <param name="billingPlatformRepository">The repository for managing billing platform data.</param>
    /// <param name="logger">The logger for logging messages related to BillingPlatformService.</param>
    public BillingPlatformService(
        IBillingPlatformRepository billingPlatformRepository,
        ILogger<BillingPlatformService> logger,
        IAuditingService auditingService)
    {
        _billingPlatformRepository = billingPlatformRepository;
        _logger = logger;
        _auditingService = auditingService;
    }


    /// <inheritdoc/>
    public async Task AddBillingPlatformAsync(BillingPlatform billingPlatform)
    {

        billingPlatform = this._auditingService.SetAuditedEntity(billingPlatform, true);
        await _billingPlatformRepository.AddAsync(billingPlatform);
        await _billingPlatformRepository.CompleteAsync();
        _logger.LogInformation($"[AddBillingPlatformAsyns] - {billingPlatform.BillingPlatformId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateBillingPlatformAsync(BillingPlatform billingPlatform)
    {
        billingPlatform = this._auditingService.SetAuditedEntity(billingPlatform, false);
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
    public async Task<IEnumerable<BillingPlatform>> GetAllBillingPlatforms()
    {
        return await _billingPlatformRepository.GetQueryable(x => x.IsDeleted == false).ToListAsync();

    }

    /// <inheritdoc/>
    public async Task DeleteBillingPlatformAsync(int billingPlatformId)
    {
        await _billingPlatformRepository.DeleteAsync(x => x.BillingPlatformId == billingPlatformId);
        await _billingPlatformRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteBillingPlatformAsync] - BillingPlatform deleted successfully for the {billingPlatformId}");
    }

}
