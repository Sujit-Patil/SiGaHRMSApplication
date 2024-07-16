using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Dto;

namespace SiGaHRMS.ApiService.Interfaces;

/// <summary>
/// LeaveRequest service interface to perform Add, Update, and delete funtionality.
/// </summary>
/// <param name="LeaveRequest"></param>
public interface ILeaveRequestService
{
    /// <summary>
    /// AddLeaveRequestAsync method will add LeaveRequest to Db
    /// </summary>
    /// <param name="leaveRequest">LeaveRequest</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task AddLeaveRequestAsync(LeaveRequest leaveRequest);

    /// <summary>
    /// UpdateLeaveRequestAsync method perform update funtionality
    /// </summary>
    /// <param name="leaveRequest">LeaveRequest</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task UpdateLeaveRequestAsync(LeaveRequest leaveRequest);

    /// <summary>
    /// DeleteLeaveRequestAsync method perform Delete funtionality
    /// </summary>
    /// <param name="leaveRequestid">LeaveRequest Id</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task DeleteLeaveRequestAsync(int leaveRequestId);

    /// <summary>
    /// GetLeaveRequestByIdAsync method gives LeaveRequest using Id
    /// </summary>
    /// <param name="id">LeaveRequest Id</param>
    /// <returns>Returns single LeaveRequest </returns>
    public Task<LeaveRequest?> GetLeaveRequestByIdAsync(int id);

    /// <summary>
    /// GetAllLeaveRequests gives list of LeaveRequests
    /// </summary>
    /// <returns>Returns list of LeaveRequest</returns>
    public List<LeaveRequest> GetAllLeaveRequests();

    /// <summary>
    /// Get LeaveRequests list according to RequestDto
    /// </summary>
    /// <param name="leaveRequestDto">RequestDto</param>
    /// <returns>Returns list of LeaveRequest</returns>
    public List<LeaveRequest> GetLeaveRequestsByDateAsync(RequestDto leaveRequestDto);

    /// <summary>
    /// Update LeaveRequest Status (Approve,Reject) using LeaveRequest
    /// </summary>
    /// <param name="leaveRequest">LeaveRequest</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task UpdateLeaveRequestStatusAsync(LeaveRequest leaveRequest);

}
