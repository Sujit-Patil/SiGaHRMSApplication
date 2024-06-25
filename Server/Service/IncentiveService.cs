using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Service;

public class IncentiveService : IIncentiveService
{
    private readonly IIncentiveRepository _incentiveRepository;
    private ILogger<IncentiveService> _logger;

    /// <summary>
    /// Initializes a new instance 
    /// </summary>
    /// <param name="IIncentiveRepository">dfhgdj</param>
    /// <param name="ILogger<IncentiveService>">gfhk</param>
    public IncentiveService(IIncentiveRepository incentiveRepository, ILogger<IncentiveService> logger)
    {
        _incentiveRepository = incentiveRepository;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task AddIncentiveAsync(Incentive incentive)
    {

        await _incentiveRepository.AddAsync(incentive);
        await _incentiveRepository.CompleteAsync();
        _logger.LogInformation($"[AddIncentiveAsyns] - {incentive.IncentiveId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateIncentiveAsync(Incentive incentive)
    {
        await _incentiveRepository.UpdateAsync(incentive);
        await _incentiveRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateIncentiveAsyns] - Incentive updated successfully for the {incentive.IncentiveId}");
    }

    /// <inheritdoc/>
    public async Task<Incentive?> GetIncentiveByIdAsync(int id)
    {
        return await _incentiveRepository.
            FirstOrDefaultAsync(x => x.IncentiveId == id);
    }

    /// <inheritdoc/>
    public List<Incentive> GetAllIncentives()
    {
        var incentiveList = _incentiveRepository.GetAll();
        return (List<Incentive>)incentiveList;
    }

    /// <inheritdoc/>
    public async Task DeleteIncentiveAsync(int incentiveId)
    {
        await _incentiveRepository.DeleteAsync(x => x.IncentiveId == incentiveId);
        await _incentiveRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteIncentiveAsync] - Incentive deleted successfully for the {incentiveId}");
    }

}
