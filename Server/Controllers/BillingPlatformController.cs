using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Constants;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Controllers;

/// <summary>BillingPlatformController
/// BillingPlatform Controller 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class BillingPlatformController : ControllerBase
{
    private readonly IBillingPlatformService _billingPlatformService;

    /// <summary>
    /// Initializes a new instance of see ref<paramref name="BillingPlatformController"/>
    /// </summary>
    /// <param name="billingPlatformService"></param>
    public BillingPlatformController(IBillingPlatformService billingPlatformService)
    {
        _billingPlatformService = billingPlatformService;
    }

    /// <summary>
    /// The controller method to retrive all BillingPlatforms.
    /// </summary>
    /// <returns>returns list of BillingPlatforms</returns>

    [HttpGet]
    public Task<IEnumerable<BillingPlatform>> GetAllBillingPlatforms()
    {
        return _billingPlatformService.GetAllBillingPlatforms();
    }

    /// <summary>
    /// Get method to retrive single BillingPlatform
    /// </summary>
    /// <param name="id">BillingPlatform Id</param>
    /// <returns> return single BillingPlatform using BillingPlatform Id</returns>
    [HttpGet("{id:int}")]
    public async Task<BillingPlatform?> GetBillingPlatformByIdAsync(int id)
    {
        return await _billingPlatformService.GetBillingPlatformByIdAsync(id);
    }

    /// <summary>
    /// Post method to Add BillingPlatform to database
    /// </summary>
    /// <param name="billingPlatform"> BillingPlatform object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPost]
    public async Task AddBillingPlatformAsync(BillingPlatform billingPlatform)
    {
        await _billingPlatformService.AddBillingPlatformAsync(billingPlatform);
    }

    /// <summary>
    /// Upadte method to Update BillingPlatform to database
    /// </summary>
    /// <param name="billingPlatform">BillingPlatform object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPut]
    public async Task UpdateBillingPlatformAsync(BillingPlatform billingPlatform)
    {
        await _billingPlatformService.UpdateBillingPlatformAsync(billingPlatform);
    }

    /// <summary>
    /// Delete method to delete BillingPlatform to database
    /// </summary>
    /// <param name="id">BillingPlatform Id</param>
    /// <returns>returns bool</returns>
    [HttpDelete("{id:int}")]
    public async Task<bool> DeleteBillingPlatformAsync(int id)
    {
        await _billingPlatformService.DeleteBillingPlatformAsync(id);
        return true;
    }
}
