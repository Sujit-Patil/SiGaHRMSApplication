using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Interfaces;

/// <summary>
/// Incentive service interface to perform Add, Update, and delete funtionality.
/// </summary>
/// <param name="Incentive"></param>
public interface IIncentiveService
{
    /// <summary>
    /// AddIncentiveAsync method will add Incentive to Db
    /// </summary>
    /// <param name="incentive">Incentive</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task AddIncentiveAsync(Incentive incentive);

    /// <summary>
    /// UpdateIncentiveAsync method perform update funtionality
    /// </summary>
    /// <param name="incentive">Incentive</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task UpdateIncentiveAsync(Incentive incentive);

    /// <summary>
    /// DeleteIncentiveAsync method perform Delete funtionality
    /// </summary>
    /// <param name="incentiveid">Incentive Id</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task DeleteIncentiveAsync(int incentiveId);

    /// <summary>
    /// GetIncentiveByIdAsync method gives Incentive using Id
    /// </summary>
    /// <param name="id">Incentive Id</param>
    /// <returns>Returns single Incentive </returns>
    public Task<Incentive?> GetIncentiveByIdAsync(int id);

    /// <summary>
    /// GetAllIncentives gives list of Incentives
    /// </summary>
    /// <returns>Returns list of Incentive</returns>
    public List<Incentive> GetAllIncentives();

}
