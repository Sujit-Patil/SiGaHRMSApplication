using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Interfaces;

/// <summary>
/// IncentivePurpose service interface to perform Add, Update, and delete funtionality.
/// </summary>
/// <param name="IncentivePurpose"></param>
public interface IIncentivePurposeService
{
    /// <summary>
    /// AddIncentivePurposeAsync method will add IncentivePurpose to Db
    /// </summary>
    /// <param name="incentivePurpose">IncentivePurpose</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task AddIncentivePurposeAsync(IncentivePurpose incentivePurpose);

    /// <summary>
    /// UpdateIncentivePurposeAsync method perform update funtionality
    /// </summary>
    /// <param name="incentivePurpose">IncentivePurpose</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task UpdateIncentivePurposeAsync(IncentivePurpose incentivePurpose);

    /// <summary>
    /// DeleteIncentivePurposeAsync method perform Delete funtionality
    /// </summary>
    /// <param name="incentivePurposeid">IncentivePurpose Id</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task DeleteIncentivePurposeAsync(int incentivePurposeId);

    /// <summary>
    /// GetIncentivePurposeByIdAsync method gives IncentivePurpose using Id
    /// </summary>
    /// <param name="id">IncentivePurpose Id</param>
    /// <returns>Returns single IncentivePurpose </returns>
    public Task<IncentivePurpose?> GetIncentivePurposeByIdAsync(int id);

    /// <summary>
    /// GetAllIncentivePurposes gives list of IncentivePurposes
    /// </summary>
    /// <returns>Returns list of IncentivePurpose</returns>
    public Task<IEnumerable<IncentivePurpose>> GetAllIncentivePurposes();

}
