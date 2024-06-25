using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Interfaces;

/// <summary>
/// BillingPlatform service interface to perform Add, Update, and delete funtionality.
/// </summary>
/// <param name="BillingPlatform"></param>
public interface IBillingPlatformService
{
    /// <summary>
    /// AddBillingPlatformAsync method will add BillingPlatform to Db
    /// </summary>
    /// <param name="billingPlatform">BillingPlatform</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task AddBillingPlatformAsync(BillingPlatform billingPlatform);

    /// <summary>
    /// UpdateBillingPlatformAsync method perform update funtionality
    /// </summary>
    /// <param name="billingPlatform">BillingPlatform</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task UpdateBillingPlatformAsync(BillingPlatform billingPlatform);

    /// <summary>
    /// DeleteBillingPlatformAsync method perform Delete funtionality
    /// </summary>
    /// <param name="billingPlatformid">BillingPlatform Id</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task DeleteBillingPlatformAsync(int billingPlatformId);

    /// <summary>
    /// GetBillingPlatformByIdAsync method gives BillingPlatform using Id
    /// </summary>
    /// <param name="id">BillingPlatform Id</param>
    /// <returns>Returns single BillingPlatform </returns>
    public Task<BillingPlatform?> GetBillingPlatformByIdAsync(int id);

    /// <summary>
    /// GetAllBillingPlatforms gives list of BillingPlatforms
    /// </summary>
    /// <returns>Returns list of BillingPlatform</returns>
    public List<BillingPlatform> GetAllBillingPlatforms();

}
