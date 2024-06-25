using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Dto;

namespace SiGaHRMS.ApiService.Interfaces;

/// <summary>
/// Attendance service interface to perform Add, Update, and delete funtionality.
/// </summary>
/// <param name="Attendance"></param>
public interface IAttendanceService
{
    /// <summary>
    /// AddAttendanceAsync method will add Attendance to Db
    /// </summary>
    /// <param name="attendance">Attendance</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task AddAttendanceAsync(Attendance attendance);

    /// <summary>
    /// UpdateAttendanceAsync method perform update funtionality
    /// </summary>
    /// <param name="attendance">Attendance</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task UpdateAttendanceAsync(Attendance attendance);

    /// <summary>
    /// DeleteAttendanceAsync method perform Delete funtionality
    /// </summary>
    /// <param name="attendanceid">Attendance Id</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task DeleteAttendanceAsync(int attendanceId);

    /// <summary>
    /// GetAttendanceByIdAsync method gives Attendance using Id
    /// </summary>
    /// <param name="id">Attendance Id</param>
    /// <returns>Returns single Attendance </returns>
    public Task<Attendance?> GetAttendanceByIdAsync(int id);

    /// <summary>
    /// GetAllAttendances gives list of Attendances
    /// </summary>
    /// <returns>Returns list of Attendance</returns>
    public List<Attendance> GetAllAttendances();

    public List<Attendance> GetAttendanceByDateAsync(RequestDto attendanceDto);

}
