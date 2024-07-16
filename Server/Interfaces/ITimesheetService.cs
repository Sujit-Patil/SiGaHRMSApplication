using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Dto;

namespace SiGaHRMS.ApiService.Interfaces;

/// <summary>
/// Timesheet service interface to perform Add, Update, and delete funtionality.
/// </summary>
/// <param name="Timesheet"></param>
public interface ITimesheetService
{
    /// <summary>
    /// AddTimesheetAsync method will add Timesheet to Db
    /// </summary>
    /// <param name="timesheet">Timesheet</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task AddTimesheetAsync(Timesheet timesheet);

    /// <summary>
    /// UpdateTimesheetAsync method perform update funtionality
    /// </summary>
    /// <param name="timesheet">Timesheet</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task UpdateTimesheetAsync(Timesheet timesheet);

    /// <summary>
    /// DeleteTimesheetAsync method perform Delete funtionality
    /// </summary>
    /// <param name="timesheetid">Timesheet Id</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task DeleteTimesheetAsync(int timesheetId);

    /// <summary>
    /// GetTimesheetByIdAsync method gives Timesheet using Id
    /// </summary>
    /// <param name="id">Timesheet Id</param>
    /// <returns>Returns single Timesheet </returns>
    public Task<Timesheet?> GetTimesheetByIdAsync(int id);

    /// <summary>
    /// GetAllTimesheets gives list of Timesheets
    /// </summary>
    /// <returns>Returns list of Timesheet</returns>
    public Task<IEnumerable<Timesheet>> GetAllTimesheets();

    /// <summary>
    /// Get Timesheets list according to RequestDto
    /// </summary>
    ///  <param name="timesheetRequestDto">RequestDto</param>
    /// <returns>Returns list of Timesheet</returns>
    public List<Timesheet> GetTimesheetsByDateAsync(RequestDto timesheetRequestDto);

}
