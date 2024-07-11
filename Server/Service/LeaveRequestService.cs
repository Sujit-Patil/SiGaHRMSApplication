using Microsoft.EntityFrameworkCore;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Dto;
using SiGaHRMS.Data.Model.Enum;

namespace SiGaHRMS.ApiService.Service;

public class LeaveRequestService : ILeaveRequestService
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IAuditingService _auditingService;
    private ILogger<LeaveRequestService> _logger;

    /// <summary>
    /// Initializes a new instance 
    /// </summary>
    /// <param name="ILeaveRequestRepository">dfhgdj</param>
    /// <param name="ILogger<LeaveRequestService>">gfhk</param>
    public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository, IAuditingService auditingService,ILogger<LeaveRequestService> logger)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _auditingService = auditingService;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task AddLeaveRequestAsync(LeaveRequest leaveRequest)
    {
        leaveRequest = _auditingService.SetAuditedEntity(leaveRequest, created: true);
        await _leaveRequestRepository.AddAsync(leaveRequest);
        await _leaveRequestRepository.CompleteAsync();
        _logger.LogInformation($"[AddLeaveRequestAsync] - {leaveRequest.LeaveRequestId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateLeaveRequestAsync(LeaveRequest leaveRequest)
    {
        leaveRequest = _auditingService.SetAuditedEntity(leaveRequest, created: false);
        await _leaveRequestRepository.UpdateAsync(leaveRequest);
        await _leaveRequestRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateLeaveRequestAsyns] - LeaveRequest updated successfully for the {leaveRequest.LeaveRequestId}");
    }

    /// <inheritdoc/>
    public async Task<LeaveRequest?> GetLeaveRequestByIdAsync(int id)
    {
        return await _leaveRequestRepository.
            FirstOrDefaultAsync(x => x.LeaveRequestId == id);
    }

    /// <inheritdoc/>
    public List<LeaveRequest> GetAllLeaveRequests()
    {
        return _leaveRequestRepository.GetQueryable(filter: x => x.IsDeleted == false, include: y => y.Include(x => x.Employee)).ToList();
    }

    /// <inheritdoc/>
    public async Task DeleteLeaveRequestAsync(int leaveRequestId)
    {
        await _leaveRequestRepository.DeleteAsync(x => x.LeaveRequestId == leaveRequestId);
        await _leaveRequestRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteLeaveRequestAsync] - LeaveRequest deleted successfully for the {leaveRequestId}");
    }

    public List<LeaveRequest> GetLeaveRequestsByDateAsync(RequestDto leaveRequestDto)
    {
        if (leaveRequestDto.FormDate == null && leaveRequestDto == null && leaveRequestDto.EmployeeId == null)
            return _leaveRequestRepository.GetQueryable(filter: x => x.ToDate >= DateOnly.FromDateTime(DateTime.Today.Date) && x.IsDeleted == false, include: y => y.Include(x => x.Employee)).ToList();
        if (leaveRequestDto?.EmployeeId == null)
            return _leaveRequestRepository.GetQueryable(filter: x => x.FromDate >= leaveRequestDto.FormDate && x.ToDate <= leaveRequestDto.ToDate && x.IsDeleted == false, include: y => y.Include(x => x.Employee)).ToList();

        return _leaveRequestRepository.GetQueryable(x => x.EmployeeId == leaveRequestDto.EmployeeId && x.FromDate >= leaveRequestDto.FormDate && x.ToDate <= leaveRequestDto.ToDate && x.IsDeleted == false, include: y => y.Include(x => x.Employee)).ToList();
    }
}
