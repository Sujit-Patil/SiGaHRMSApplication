using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.ApiService.Service;
using SiGaHRMS.Data.Constants;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Dto;

namespace SiGaHRMS.ApiService.Controllers;

/// <summary>
/// LeaveRequest Controller 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class LeaveRequestController : ControllerBase
{
    private readonly ILeaveRequestService _leaveRequestService;

    /// <summary>
    /// Initializes a new instance of see ref<paramref name="LeaveRequestController"/>
    /// </summary>
    /// <param name="leaveRequestService"></param>
    public LeaveRequestController(ILeaveRequestService leaveRequestService)
    {
        _leaveRequestService = leaveRequestService;
    }

    /// <summary>
    /// The controller method to retrive all LeaveRequests.
    /// </summary>
    /// <returns>returns list of LeaveRequests</returns>
    
    [HttpGet]
    [Authorize(Roles =RoleConstants.SUPERADMIN)]
    public List<LeaveRequest> GetAllLeaveRequests()
    {
        return _leaveRequestService.GetAllLeaveRequests();
    }

    /// <summary>
    /// Get method to retrive single LeaveRequest
    /// </summary>
    /// <param name="id">LeaveRequest Id</param>
    /// <returns> return single LeaveRequest using LeaveRequest Id</returns>
    [HttpGet("{id:int}")]
    public async Task<LeaveRequest?> GetLeaveRequestByIdAsync(int id)
    {
        return await _leaveRequestService.GetLeaveRequestByIdAsync(id);
    }

    /// <summary>
    /// Post method to Add LeaveRequest to database
    /// </summary>
    /// <param name="leaveRequest"> LeaveRequest object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPost]
    public async Task AddLeaveRequestAsync(LeaveRequest leaveRequest)
    {
        await _leaveRequestService.AddLeaveRequestAsync(leaveRequest);
    }

    /// <summary>
    /// Get method to retrive LeaveRequest By Date
    /// </summary>
    /// <param name="LeaveRequest Dto">leaveRequestDto</param>
    /// <returns> return List of Leave using Date</returns>
    [HttpPost("ByDate")]
    public List<LeaveRequest> GetLeaveByDateAsync(RequestDto leaveRequestDto)
    {
        return _leaveRequestService.GetLeaveRequestsByDateAsync(leaveRequestDto);
    }

    /// <summary>
    /// Upadte method to Update LeaveRequest to database
    /// </summary>
    /// <param name="leaveRequest">LeaveRequest object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPut]
    public async Task UpdateLeaveRequestAsync(LeaveRequest leaveRequest)
    {
        await _leaveRequestService.UpdateLeaveRequestAsync(leaveRequest);
    }

    /// <summary>
    /// Delete method to delete LeaveRequest to database
    /// </summary>
    /// <param name="id">LeaveRequest Id</param>
    /// <returns>returns bool</returns>
    [HttpDelete("{id:int}")]
    public async Task<bool> DeleteLeaveRequestAsync(int id)
    {
        await _leaveRequestService.DeleteLeaveRequestAsync(id);
        return true;
    }
}
