using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Service;

public class LeaveBalanceService : ILeaveBalanceService
{
    private readonly ILeaveBalanceRepository _leaveBalanceRepository;
    private ILogger<LeaveBalanceService> _logger;

    /// <summary>
    /// Initializes a new instance 
    /// </summary>
    /// <param name="ILeaveBalanceRepository">dfhgdj</param>
    /// <param name="ILogger<LeaveBalanceService>">gfhk</param>
    public LeaveBalanceService(ILeaveBalanceRepository leaveBalanceRepository, ILogger<LeaveBalanceService> logger)
    {
        _leaveBalanceRepository = leaveBalanceRepository;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task AddLeaveBalanceAsync(LeaveBalance leaveBalance)
    {

        await _leaveBalanceRepository.AddAsync(leaveBalance);
        await _leaveBalanceRepository.CompleteAsync();
        _logger.LogInformation($"[AddLeaveBalanceAsyns] - {leaveBalance.LeaveBalanceId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateLeaveBalanceAsync(LeaveBalance leaveBalance)
    {
        await _leaveBalanceRepository.UpdateAsync(leaveBalance);
        await _leaveBalanceRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateLeaveBalanceAsyns] - LeaveBalance updated successfully for the {leaveBalance.LeaveBalanceId}");
    }

    /// <inheritdoc/>
    public async Task<LeaveBalance?> GetLeaveBalanceByIdAsync(int id)
    {
        return await _leaveBalanceRepository.
            FirstOrDefaultAsync(x => x.LeaveBalanceId == id);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<LeaveBalance>> GetAllLeaveBalances()
    {
       return _leaveBalanceRepository.GetAllAsync();


    }

    /// <inheritdoc/>
    public async Task DeleteLeaveBalanceAsync(int leaveBalanceId)
    {
        await _leaveBalanceRepository.DeleteAsync(x => x.LeaveBalanceId == leaveBalanceId);
        await _leaveBalanceRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteLeaveBalanceAsync] - LeaveBalance deleted successfully for the {leaveBalanceId}");
    }

}
