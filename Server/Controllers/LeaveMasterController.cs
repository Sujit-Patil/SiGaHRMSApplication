using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Constants;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Controllers;

/// <summary>
/// LeaveMaster Controller 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class LeaveMasterController : ControllerBase
{
    private readonly ILeaveMasterService _leaveMasterService;

    /// <summary>
    /// Initializes a new instance of see ref<paramref name="LeaveMasterController"/>
    /// </summary>
    /// <param name="leaveMasterService"></param>
    public LeaveMasterController(ILeaveMasterService leaveMasterService)
    {
        _leaveMasterService = leaveMasterService;
    }

    /// <summary>
    /// The controller method to retrive all LeaveMasters.
    /// </summary>
    /// <returns>returns list of LeaveMasters</returns>
    
    [HttpGet]
    public Task<IEnumerable<LeaveMaster>> GetAllLeaveMasters()
    {
        return _leaveMasterService.GetAllLeaveMasters();
    }

    /// <summary>
    /// Get method to retrive single LeaveMaster
    /// </summary>
    /// <param name="id">LeaveMaster Id</param>
    /// <returns> return single LeaveMaster using LeaveMaster Id</returns>
    [HttpGet("{id:int}")]
    public async Task<LeaveMaster?> GetLeaveMasterByIdAsync(int id)
    {
        return await _leaveMasterService.GetLeaveMasterByIdAsync(id);
    }

    /// <summary>
    /// Post method to Add LeaveMaster to database
    /// </summary>
    /// <param name="leaveMaster"> LeaveMaster object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPost]
    public async Task AddLeaveMasterAsync(LeaveMaster leaveMaster)
    {
        await _leaveMasterService.AddLeaveMasterAsync(leaveMaster);
    }

    /// <summary>
    /// Upadte method to Update LeaveMaster to database
    /// </summary>
    /// <param name="leaveMaster">LeaveMaster object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPut]
    public async Task UpdateLeaveMasterAsync(LeaveMaster leaveMaster)
    {
        await _leaveMasterService.UpdateLeaveMasterAsync(leaveMaster);
    }

    /// <summary>
    /// Delete method to delete LeaveMaster to database
    /// </summary>
    /// <param name="id">LeaveMaster Id</param>
    /// <returns>returns bool</returns>
    [HttpDelete("{id:int}")]
    public async Task<bool> DeleteLeaveMasterAsync(int id)
    {
        await _leaveMasterService.DeleteLeaveMasterAsync(id);
        return true;
    }
}
