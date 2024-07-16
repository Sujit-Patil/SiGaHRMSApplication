using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Dto;

namespace SiGaHRMS.ApiService.Interfaces;

/// <summary>
/// TimeSheetDetail service interface to perform Add, Update, and delete funtionality.
/// </summary>
/// <param name="TimeSheetDetail"></param>
public interface ITimeSheetDetailService
{
    /// <summary>
    /// AddTimeSheetDetailAsync method will add TimeSheetDetail to Db
    /// </summary>
    /// <param name="timeSheetDetail">TimeSheetDetail</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task AddTimeSheetDetailAsync(TimeSheetDetail timeSheetDetail);

    /// <summary>
    /// UpdateTimeSheetDetailAsync method perform update funtionality
    /// </summary>
    /// <param name="timeSheetDetail">TimeSheetDetail</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task UpdateTimeSheetDetailAsync(TimeSheetDetail timeSheetDetail);

    /// <summary>
    /// DeleteTimeSheetDetailAsync method perform Delete funtionality
    /// </summary>
    /// <param name="timeSheetDetailid">TimeSheetDetail Id</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task DeleteTimeSheetDetailAsync(int timeSheetDetailId);

    /// <summary>
    /// GetTimeSheetDetailByIdAsync method gives TimeSheetDetail using Id
    /// </summary>
    /// <param name="id">TimeSheetDetail Id</param>
    /// <returns>Returns single TimeSheetDetail </returns>
    public Task<TimeSheetDetail?> GetTimeSheetDetailByIdAsync(int id);

    /// <summary>
    /// GetAllTimeSheetDetails gives list of TimeSheetDetails
    /// </summary>
    /// <returns>Returns list of TimeSheetDetail</returns>
    public Task<IEnumerable<TimeSheetDetail>> GetAllTimeSheetDetails();

    /// <summary>
    /// Get TimeSheetDetails list according to RequestDto
    /// </summary>
    /// <param name="timesheetRequestDto">RequestDto</param>
    /// <returns>Returns list of TimeSheetDetail</returns>
    public List<TimeSheetDetail> GetTimesheetDetailByDateAsync(RequestDto timesheetRequestDto);

}
