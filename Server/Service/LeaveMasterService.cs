using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Service;

public class LeaveMasterService : ILeaveMasterService
{
    private readonly ILeaveMasterRepository _leaveMasterRepository;
    private ILogger<LeaveMasterService> _logger;

    /// <summary>
    /// Initializes a new instance 
    /// </summary>
    /// <param name="ILeaveMasterRepository">dfhgdj</param>
    /// <param name="ILogger<LeaveMasterService>">gfhk</param>
    public LeaveMasterService(ILeaveMasterRepository leaveMasterRepository, ILogger<LeaveMasterService> logger)
    {
        _leaveMasterRepository = leaveMasterRepository;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task AddLeaveMasterAsync(LeaveMaster leaveMaster)
    {

        await _leaveMasterRepository.AddAsync(leaveMaster);
        await _leaveMasterRepository.CompleteAsync();
        _logger.LogInformation($"[AddLeaveMasterAsyns] - {leaveMaster.LeaveMasterId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateLeaveMasterAsync(LeaveMaster leaveMaster)
    {
        await _leaveMasterRepository.UpdateAsync(leaveMaster);
        await _leaveMasterRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateLeaveMasterAsyns] - LeaveMaster updated successfully for the {leaveMaster.LeaveMasterId}");
    }

    /// <inheritdoc/>
    public async Task<LeaveMaster?> GetLeaveMasterByIdAsync(int id)
    {
        return await _leaveMasterRepository.
            FirstOrDefaultAsync(x => x.LeaveMasterId == id);
    }

    /// <inheritdoc/>
    public List<LeaveMaster> GetAllLeaveMasters()
    {
        var leaveMasterList = _leaveMasterRepository.GetAll();
        return (List<LeaveMaster>)leaveMasterList;
    }

    /// <inheritdoc/>
    public async Task DeleteLeaveMasterAsync(int leaveMasterId)
    {
        await _leaveMasterRepository.DeleteAsync(x => x.LeaveMasterId == leaveMasterId);
        await _leaveMasterRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteLeaveMasterAsync] - LeaveMaster deleted successfully for the {leaveMasterId}");
    }

}
