using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Interfaces;

/// <summary>
/// LeaveMaster service interface to perform Add, Update, and delete funtionality.
/// </summary>
/// <param name="LeaveMaster"></param>
public interface ILeaveMasterService
{
    /// <summary>
    /// AddLeaveMasterAsync method will add LeaveMaster to Db
    /// </summary>
    /// <param name="leaveMaster">LeaveMaster</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task AddLeaveMasterAsync(LeaveMaster leaveMaster);

    /// <summary>
    /// UpdateLeaveMasterAsync method perform update funtionality
    /// </summary>
    /// <param name="leaveMaster">LeaveMaster</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task UpdateLeaveMasterAsync(LeaveMaster leaveMaster);

    /// <summary>
    /// DeleteLeaveMasterAsync method perform Delete funtionality
    /// </summary>
    /// <param name="leaveMasterid">LeaveMaster Id</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task DeleteLeaveMasterAsync(int leaveMasterId);

    /// <summary>
    /// GetLeaveMasterByIdAsync method gives LeaveMaster using Id
    /// </summary>
    /// <param name="id">LeaveMaster Id</param>
    /// <returns>Returns single LeaveMaster </returns>
    public Task<LeaveMaster?> GetLeaveMasterByIdAsync(int id);

    /// <summary>
    /// GetAllLeaveMasters gives list of LeaveMasters
    /// </summary>
    /// <returns>Returns list of LeaveMaster</returns>
    public Task<IEnumerable<LeaveMaster>> GetAllLeaveMasters();

}
