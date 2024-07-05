using Microsoft.EntityFrameworkCore;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Dto;
using SiGaHRMS.Data.Model.Enum;

namespace SiGaHRMS.ApiService.Service;

public class AttendanceService : IAttendanceService
{
    private readonly IAttendanceRepository _attendanceRepository;
    private ILogger<AttendanceService> _logger;

    /// <summary>
    /// Initializes a new instance 
    /// </summary>
    /// <param name="IAttendanceRepository">dfhgdj</param>
    /// <param name="ILogger<AttendanceService>">gfhk</param>
    public AttendanceService(IAttendanceRepository attendanceRepository, ILogger<AttendanceService> logger)
    {
        _attendanceRepository = attendanceRepository;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task AddAttendanceAsync(Attendance attendance)
    {

        await _attendanceRepository.AddAsync(attendance);
        await _attendanceRepository.CompleteAsync();
        _logger.LogInformation($"[AddAttendanceAsyns] - {attendance.AttendanceId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateAttendanceAsync(Attendance attendance)
    {
        await _attendanceRepository.UpdateAsync(attendance);
        await _attendanceRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateAttendanceAsyns] - Attendance updated successfully for the {attendance.AttendanceId}");
    }

    /// <inheritdoc/>
    public async Task<Attendance?> GetAttendanceByIdAsync(int id)
    {
        return await _attendanceRepository.
            FirstOrDefaultAsync(x => x.AttendanceId == id);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<Attendance>> GetAllAttendances()
    {
        return _attendanceRepository.GetAllAsync();
    }

    /// <inheritdoc/>
    public async Task DeleteAttendanceAsync(int attendanceId)
    {
        await _attendanceRepository.DeleteAsync(x => x.AttendanceId == attendanceId);
        await _attendanceRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteAttendanceAsync] - Attendance deleted successfully for the {attendanceId}");
    }

    public List<Attendance> GetAttendanceByDateAsync(RequestDto attendanceDto)
    {
        if (attendanceDto?.EmployeeId == null)
            return _attendanceRepository.GetQueryable(filter: x => x.AttendanceDate >= attendanceDto.FormDate && x.AttendanceDate <= attendanceDto.ToDate && x.IsDeleted == false, include: x => x.Include(x => x.Employee)).ToList();

        return _attendanceRepository.GetQueryable(filter: x => x.EmployeeId == attendanceDto.EmployeeId && x.AttendanceDate >= attendanceDto.FormDate && x.AttendanceDate <= attendanceDto.ToDate, include: x => x.Include(x => x.Employee)).ToList();
    }

}
