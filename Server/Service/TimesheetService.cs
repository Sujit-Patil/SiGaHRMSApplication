using Microsoft.EntityFrameworkCore;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Dto;

namespace SiGaHRMS.ApiService.Service;

public class TimesheetService : ITimesheetService
{
    private readonly ITimesheetRepository _timesheetRepository;
    private ILogger<TimesheetService> _logger;

    /// <summary>
    /// Initializes a new instance of the TimesheetService class.
    /// </summary>
    /// <param name="timesheetRepository">The repository for managing timesheet data.</param>
    /// <param name="logger">The logger for logging messages related to TimesheetService.</param>
    public TimesheetService(
        ITimesheetRepository timesheetRepository,
        ILogger<TimesheetService> logger)
    {
        _timesheetRepository = timesheetRepository;
        _logger = logger;
    }


    /// <inheritdoc/>
    public async Task AddTimesheetAsync(Timesheet timesheet)
    {

        await _timesheetRepository.AddAsync(timesheet);
        await _timesheetRepository.CompleteAsync();
        _logger.LogInformation($"[AddTimesheetAsyns] - {timesheet.TimesheetId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateTimesheetAsync(Timesheet timesheet)
    {
        await _timesheetRepository.UpdateAsync(timesheet);
        await _timesheetRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateTimesheetAsyns] - Timesheet updated successfully for the {timesheet.TimesheetId}");
    }

    /// <inheritdoc/>
    public async Task<Timesheet?> GetTimesheetByIdAsync(int id)
    {
        return await _timesheetRepository.
            FirstOrDefaultAsync(x => x.TimesheetId == id);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<Timesheet>> GetAllTimesheets()
    {
        return _timesheetRepository.GetAllAsync();
    }

    /// <inheritdoc/>
    public async Task DeleteTimesheetAsync(int timesheetId)
    {
        await _timesheetRepository.DeleteAsync(x => x.TimesheetId == timesheetId);
        await _timesheetRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteTimesheetAsync] - Timesheet deleted successfully for the {timesheetId}");
    }

    /// <inheritdoc/>
    public List<Timesheet> GetTimesheetsByDateAsync(RequestDto timesheetDto)
    {
        if (timesheetDto?.EmployeeId == null)
            return _timesheetRepository.GetQueryable(filter: x => x.TimesheetDate >= timesheetDto.FormDate && x.TimesheetDate <= timesheetDto.ToDate && x.IsDeleted == false, include: x => x.Include(x => x.Employee)).ToList();

        return _timesheetRepository.GetQueryable(filter: x => x.EmployeeId == timesheetDto.EmployeeId && x.TimesheetDate >= timesheetDto.FormDate && x.TimesheetDate <= timesheetDto.ToDate, include: x => x.Include(x => x.Employee)).ToList();
    }
}
