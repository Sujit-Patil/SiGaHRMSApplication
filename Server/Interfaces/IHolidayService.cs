using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Dto;

namespace SiGaHRMS.ApiService.Interfaces;

/// <summary>
/// Holiday service interface to perform Add, Update, and delete funtionality.
/// </summary>
/// <param name="Holiday"></param>
public interface IHolidayService
{
    /// <summary>
    /// AddHolidayAsync method will add Holiday to Db
    /// </summary>
    /// <param name="holiday">Holiday</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task AddHolidayAsync(Holiday holiday);

    /// <summary>
    /// UpdateHolidayAsync method perform update funtionality
    /// </summary>
    /// <param name="holiday">Holiday</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task UpdateHolidayAsync(Holiday holiday);

    /// <summary>
    /// DeleteHolidayAsync method perform Delete funtionality
    /// </summary>
    /// <param name="holidayid">Holiday Id</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task DeleteHolidayAsync(int holidayId);

    /// <summary>
    /// GetHolidayByIdAsync method gives Holiday using Id
    /// </summary>
    /// <param name="id">Holiday Id</param>
    /// <returns>Returns single Holiday </returns>
    public Task<Holiday?> GetHolidayByIdAsync(int id);

    /// <summary>
    /// GetAllHolidays gives list of Holidays
    /// </summary>
    /// <returns>Returns list of Holiday</returns>
    public Task<IEnumerable<Holiday>> GetAllHolidays();

    List<Holiday> GetHolidaysByDateAsync(RequestDto holidayRequestDto);

}
