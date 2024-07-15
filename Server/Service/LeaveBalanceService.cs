using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Service;

public class LeaveBalanceService : ILeaveBalanceService
{
    private readonly ILeaveBalanceRepository _leaveBalanceRepository;
    private readonly IAuditingService _auditingService;
    private ILogger<LeaveBalanceService> _logger;

    /// <summary>
    /// Initializes a new instance 
    /// </summary>
    /// <param name="ILeaveBalanceRepository">dfhgdj</param>
    /// <param name="ILogger<LeaveBalanceService>">gfhk</param>
    public LeaveBalanceService(ILeaveBalanceRepository leaveBalanceRepository, IAuditingService auditingService, ILogger<LeaveBalanceService> logger)
    {
        _leaveBalanceRepository = leaveBalanceRepository;
        _auditingService = auditingService;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task AddLeaveBalanceAsync(LeaveBalance leaveBalance)
    {
        leaveBalance = _auditingService.SetAuditedEntity(leaveBalance, created: true);
        await _leaveBalanceRepository.AddAsync(leaveBalance);
        await _leaveBalanceRepository.CompleteAsync();
        _logger.LogInformation($"[AddLeaveBalanceAsyns] - {leaveBalance.LeaveBalanceId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateLeaveBalanceAsync(LeaveBalance leaveBalance)
    {
        leaveBalance = _auditingService.SetAuditedEntity(leaveBalance, created: false);
        await _leaveBalanceRepository.UpdateAsync(leaveBalance);
        await _leaveBalanceRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateLeaveBalanceAsyns] - LeaveBalance updated successfully for the {leaveBalance.LeaveBalanceId}");
    }

    /// <inheritdoc/>
    public async Task<LeaveBalance?> GetLeaveBalanceByIdAsync(long id)
    {
        return await _leaveBalanceRepository.
            FirstOrDefaultAsync(x => x.EmployeeId == id);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<LeaveBalance>> GetAllLeaveBalances()
    {
        return _leaveBalanceRepository.GetAllAsync("Employee");
    }

    /// <inheritdoc/>
    public async Task DeleteLeaveBalanceAsync(int leaveBalanceId)
    {
        await _leaveBalanceRepository.DeleteAsync(x => x.LeaveBalanceId == leaveBalanceId);
        await _leaveBalanceRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteLeaveBalanceAsync] - LeaveBalance deleted successfully for the {leaveBalanceId}");
    }

}
