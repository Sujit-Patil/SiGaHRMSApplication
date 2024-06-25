using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Constants;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Controllers;

/// <summary>IncentiveController
/// Incentive Controller 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class IncentiveController : ControllerBase
{
    private readonly IIncentiveService _incentiveService;

    /// <summary>
    /// Initializes a new instance of see ref<paramref name="IncentiveController"/>
    /// </summary>
    /// <param name="incentiveService"></param>
    public IncentiveController(IIncentiveService incentiveService)
    {
        _incentiveService = incentiveService;
    }

    /// <summary>
    /// The controller method to retrive all Incentives.
    /// </summary>
    /// <returns>returns list of Incentives</returns>
    
    [HttpGet]
    [Authorize(Roles =RoleConstants.SUPERADMIN)]
    public List<Incentive> GetAllIncentives()
    {
        return _incentiveService.GetAllIncentives();
    }

    /// <summary>
    /// Get method to retrive single Incentive
    /// </summary>
    /// <param name="id">Incentive Id</param>
    /// <returns> return single Incentive using Incentive Id</returns>
    [HttpGet("{id:int}")]
    public async Task<Incentive?> GetIncentiveByIdAsync(int id)
    {
        return await _incentiveService.GetIncentiveByIdAsync(id);
    }

    /// <summary>
    /// Post method to Add Incentive to database
    /// </summary>
    /// <param name="incentive"> Incentive object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPost]
    public async Task AddIncentiveAsync(Incentive incentive)
    {
        await _incentiveService.AddIncentiveAsync(incentive);
    }

    /// <summary>
    /// Upadte method to Update Incentive to database
    /// </summary>
    /// <param name="incentive">Incentive object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPut]
    public async Task UpdateIncentiveAsync(Incentive incentive)
    {
        await _incentiveService.UpdateIncentiveAsync(incentive);
    }

    /// <summary>
    /// Delete method to delete Incentive to database
    /// </summary>
    /// <param name="id">Incentive Id</param>
    /// <returns>returns bool</returns>
    [HttpDelete("{id:int}")]
    public async Task<bool> DeleteIncentiveAsync(int id)
    {
        await _incentiveService.DeleteIncentiveAsync(id);
        return true;
    }
}
