using Microsoft.EntityFrameworkCore;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Dto;

namespace SiGaHRMS.ApiService.Service;

public class LeaveRequestService : ILeaveRequestService
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private ILogger<LeaveRequestService> _logger;

    /// <summary>
    /// Initializes a new instance 
    /// </summary>
    /// <param name="ILeaveRequestRepository">dfhgdj</param>
    /// <param name="ILogger<LeaveRequestService>">gfhk</param>
    public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository, ILogger<LeaveRequestService> logger)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task AddLeaveRequestAsync(LeaveRequest leaveRequest)
    {

        await _leaveRequestRepository.AddAsync(leaveRequest);
        await _leaveRequestRepository.CompleteAsync();
        _logger.LogInformation($"[AddLeaveRequestAsyns] - {leaveRequest.LeaveRequestId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateLeaveRequestAsync(LeaveRequest leaveRequest)
    {
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
        if (leaveRequestDto.FormDate == null || leaveRequestDto == null || leaveRequestDto.EmployeeId == null)
            return _leaveRequestRepository.GetQueryable(filter: x => x.ToDate >= DateOnly.FromDateTime(DateTime.Today.Date) && x.IsDeleted == false, include: y => y.Include(x => x.Employee)).ToList();
        if (leaveRequestDto?.EmployeeId == null)
            return _leaveRequestRepository.GetQueryable(filter: x => x.FromDate >= leaveRequestDto.FormDate && x.ToDate <= leaveRequestDto.ToDate, include: y => y.Include(x => x.Employee)).ToList();

        return _leaveRequestRepository.GetQueryable(x => x.EmployeeId == leaveRequestDto.EmployeeId && x.FromDate >= leaveRequestDto.FormDate && x.ToDate <= leaveRequestDto.ToDate).ToList();
    }

}
