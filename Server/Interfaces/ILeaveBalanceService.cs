using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Interfaces;

/// <summary>
/// LeaveBalance service interface to perform Add, Update, and delete funtionality.
/// </summary>
/// <param name="LeaveBalance"></param>
public interface ILeaveBalanceService
{
    /// <summary>
    /// AddLeaveBalanceAsync method will add LeaveBalance to Db
    /// </summary>
    /// <param name="leaveBalance">LeaveBalance</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task AddLeaveBalanceAsync(LeaveBalance leaveBalance);

    /// <summary>
    /// UpdateLeaveBalanceAsync method perform update funtionality
    /// </summary>
    /// <param name="leaveBalance">LeaveBalance</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task UpdateLeaveBalanceAsync(LeaveBalance leaveBalance);

    /// <summary>
    /// DeleteLeaveBalanceAsync method perform Delete funtionality
    /// </summary>
    /// <param name="leaveBalanceid">LeaveBalance Id</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task DeleteLeaveBalanceAsync(int leaveBalanceId);

    /// <summary>
    /// GetLeaveBalanceByIdAsync method gives LeaveBalance using Id
    /// </summary>
    /// <param name="id">LeaveBalance Id</param>
    /// <returns>Returns single LeaveBalance </returns>
    public Task<LeaveBalance?> GetLeaveBalanceByIdAsync(int id);

    /// <summary>
    /// GetAllLeaveBalances gives list of LeaveBalances
    /// </summary>
    /// <returns>Returns list of LeaveBalance</returns>
    public List<LeaveBalance> GetAllLeaveBalances();

}
