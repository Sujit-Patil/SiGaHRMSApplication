using Microsoft.EntityFrameworkCore;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Dto;
using SiGaHRMS.Data.Model.Enum;
using System.Linq;

namespace SiGaHRMS.ApiService.Service;

public class LeaveRequestService : ILeaveRequestService
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveBalanceRepository _leaveBalanceRepository;
    private readonly ILeaveBalanceService _leaveBalanceService;
    private readonly IAuditingService _auditingService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<LeaveRequestService> _logger;

    public LeaveRequestService(
        ILeaveRequestRepository leaveRequestRepository,
        ILeaveBalanceRepository leaveBalanceRepository,
        ILeaveBalanceService leaveBalanceService,
        IAuditingService auditingService,
        IDateTimeProvider dateTimeProvider,
        ILogger<LeaveRequestService> logger)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveBalanceRepository = leaveBalanceRepository;
        _leaveBalanceService = leaveBalanceService;
        _auditingService = auditingService;
        _dateTimeProvider = dateTimeProvider;
        _logger = logger;
    }

    public async Task AddLeaveRequestAsync(LeaveRequest leaveRequest)
    {
        var leaveBalance = await _leaveBalanceService.GetLeaveBalanceByIdAsync(leaveRequest.EmployeeId);
        ArgumentNullException.ThrowIfNull(leaveBalance);

        SetLeaveTypeIfNotApplicable(leaveRequest, leaveBalance);
        await AddOrUpdateLeaveRequestAsync(leaveRequest, true);
    }

    public async Task UpdateLeaveRequestAsync(LeaveRequest leaveRequest)
    {
        var existingLeaveRequest = await _leaveRequestRepository.FirstOrDefaultAsync(x => x.LeaveRequestId == leaveRequest.LeaveRequestId);
        var leaveBalance = await _leaveBalanceService.GetLeaveBalanceByIdAsync(leaveRequest.EmployeeId);
        if (leaveBalance == null) return;
        if (existingLeaveRequest == null) return;

        SetLeaveTypeIfNotApplicable(existingLeaveRequest, leaveBalance);

        if (existingLeaveRequest.LeaveRequestStatus == LeaveRequestStatus.Approved && leaveRequest.IsDeleted || existingLeaveRequest.LeaveRequestStatus == LeaveRequestStatus.Approved)
        {
            var previousDays = _dateTimeProvider.CalculateDateDifferenceInDays(existingLeaveRequest.FromDate, existingLeaveRequest.ToDate);
            await UpdateLeaveBalanceAsync(leaveBalance, existingLeaveRequest.LeaveType, previousDays, true);
        }

        existingLeaveRequest.LeaveRequestStatus = leaveRequest.IsDeleted ? leaveRequest.LeaveRequestStatus : LeaveRequestStatus.Open;
        existingLeaveRequest.IsDeleted= leaveRequest.IsDeleted;
        existingLeaveRequest.FromDate = leaveRequest.FromDate;
        existingLeaveRequest.ToDate = leaveRequest.ToDate;
        existingLeaveRequest.LeaveType = leaveRequest.LeaveType;

        await AddOrUpdateLeaveRequestAsync(existingLeaveRequest, false);
    }

    public async Task UpdateLeaveRequestStatusAsync(LeaveRequest leaveRequest)
    {
        if (leaveRequest.LeaveRequestStatus == LeaveRequestStatus.Approved)
        {
            var days = _dateTimeProvider.CalculateDateDifferenceInDays(leaveRequest.FromDate, leaveRequest.ToDate);
            var leaveBalance = await _leaveBalanceService.GetLeaveBalanceByIdAsync(leaveRequest.EmployeeId);
            if (leaveBalance == null) return;

            await UpdateLeaveBalanceAsync(leaveBalance, leaveRequest.LeaveType, days, false);
        }

        await AddOrUpdateLeaveRequestAsync(leaveRequest, false);
    }

    public async Task<LeaveRequest?> GetLeaveRequestByIdAsync(int id)
    {
        return await _leaveRequestRepository.FirstOrDefaultAsync(x => x.LeaveRequestId == id);
    }

    public List<LeaveRequest> GetAllLeaveRequests()
    {
        return _leaveRequestRepository.GetQueryable(x => !x.IsDeleted, y => y.Include(x => x.Employee)).ToList();
    }

    public async Task DeleteLeaveRequestAsync(int leaveRequestId)
    {
        await _leaveRequestRepository.DeleteAsync(x => x.LeaveRequestId == leaveRequestId);
        await _leaveRequestRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteLeaveRequestAsync] - LeaveRequest {leaveRequestId} deleted successfully");
    }

    public List<LeaveRequest> GetLeaveRequestsByDateAsync(RequestDto leaveRequestDto)
    {
        var query = _leaveRequestRepository.GetQueryable(x => !x.IsDeleted, y => y.Include(x => x.Employee));

        if (leaveRequestDto?.EmployeeId != null)
            query = query.Where(x => x.EmployeeId == leaveRequestDto.EmployeeId);

        if (leaveRequestDto?.FormDate != null)
            query = query.Where(x => x.FromDate >= leaveRequestDto.FormDate);

        if (leaveRequestDto?.ToDate != null)
            query = query.Where(x => x.ToDate <= leaveRequestDto.ToDate);
        else
            query = query.Where(x => x.ToDate >= DateOnly.FromDateTime(DateTime.Today));

        return query.ToList();
    }

    #region Private Methods

    private void SetLeaveTypeIfNotApplicable(LeaveRequest leaveRequest, LeaveBalance leaveBalance)
    {
        if (leaveBalance.LeaveBalanceStatus == LeaveBalanceStatus.NotApplicable)
            leaveRequest.LeaveType = LeaveType.LossOfPay;
    }

    private async Task AddOrUpdateLeaveRequestAsync(LeaveRequest leaveRequest, bool isNew)
    {
        leaveRequest = _auditingService.SetAuditedEntity(leaveRequest, created: isNew);
        if (isNew)
            await _leaveRequestRepository.AddAsync(leaveRequest);
        else
            await _leaveRequestRepository.UpdateAsync(leaveRequest);

        await _leaveRequestRepository.CompleteAsync();
        _logger.LogInformation($"[AddOrUpdateLeaveRequestAsync] - LeaveRequest {leaveRequest.LeaveRequestId} {(isNew ? "added" : "updated")} successfully");
    }

    private async Task UpdateLeaveBalanceAsync(LeaveBalance leaveBalance, LeaveType leaveType, short days, bool isRollback)
    {
        var adjustment = (short)(isRollback ? -days : days);

        switch (leaveType)
        {
            case LeaveType.SickLeave:
                leaveBalance.SickLeavesAvailaed += adjustment;
                break;
            case LeaveType.CasualLeave:
                leaveBalance.CasualLeavesAvailaed += adjustment;
                break;
            case LeaveType.MaternityLeave:
                leaveBalance.MaternityLeavesAvailaed += adjustment;
                break;
            case LeaveType.BereavementLeave:
                leaveBalance.BereavementLeavesAvailaed += adjustment;
                break;
            case LeaveType.PaternityLeave:
                leaveBalance.PaternityLeavesAvailaed += adjustment;
                break;
            case LeaveType.CompensatoryOff:
                leaveBalance.CompensatoryOffsAvailaed += adjustment;
                break;
            case LeaveType.EarnedLeave:
                leaveBalance.EarnedLeavesAvailaed += adjustment;
                break;
            case LeaveType.MarriageLeave:
                leaveBalance.MarriageLeavesAvailaed += adjustment;
                break;
            default:
                leaveBalance.LossofPayLeavesAvailaed += adjustment;
                break;
        }

        await _leaveBalanceService.UpdateLeaveBalanceAsync(leaveBalance);
    }

    #endregion
}
