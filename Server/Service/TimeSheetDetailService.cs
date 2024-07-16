using Microsoft.EntityFrameworkCore;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Dto;
using SiGaHRMS.Data.Model.Enum;

namespace SiGaHRMS.ApiService.Service;

public class TimeSheetDetailService : ITimeSheetDetailService
{
    private readonly ITimeSheetDetailRepository _timeSheetDetailRepository;
    private readonly ITimesheetRepository _timeSheetRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ISessionService _sessionService;
    private readonly IAuditingService _auditingService;
    private ILogger<TimeSheetDetailService> _logger;

    /// <summary>
    /// Initializes a new instance of the TimeSheetDetailService class.
    /// </summary>
    /// <param name="sessionService">The service for managing session-related operations.</param>
    /// <param name="auditingService">The service for auditing operations.</param>
    /// <param name="timeSheetDetailRepository">The repository for managing timesheet detail data.</param>
    /// <param name="dateTimeProvider">The provider for date and time operations.</param>
    /// <param name="timeSheetRepository">The repository for managing timesheet data.</param>
    /// <param name="logger">The logger for logging messages related to TimeSheetDetailService.</param>
    public TimeSheetDetailService(
        ISessionService sessionService,
        IAuditingService auditingService,
        ITimeSheetDetailRepository timeSheetDetailRepository,
        IDateTimeProvider dateTimeProvider,
        ITimesheetRepository timeSheetRepository,
        ILogger<TimeSheetDetailService> logger)
    {
        _timeSheetDetailRepository = timeSheetDetailRepository;
        _sessionService = sessionService;
        _auditingService = auditingService;
        _dateTimeProvider = dateTimeProvider;
        _timeSheetRepository = timeSheetRepository;
        _logger = logger;
    }


    /// <inheritdoc/>
    public async Task AddTimeSheetDetailAsync(TimeSheetDetail timeSheetDetail)
    {
        var employeeId = _sessionService.GetCurrentEmployeeId();
        var timesheet = await GetOrCreateTimesheetAsync(timeSheetDetail, employeeId);

        timeSheetDetail.TimesheetId ??= timesheet.TimesheetId;
        timeSheetDetail = _auditingService.SetAuditedEntity(timeSheetDetail, created: true);

        await _timeSheetDetailRepository.AddAsync(timeSheetDetail);
        await _timeSheetDetailRepository.CompleteAsync();
        _logger.LogInformation($"[AddTimeSheetDetailAsync] - TimeSheetDetail {timeSheetDetail.TimeSheetDetailId} added successfully by employeeId {employeeId}");
    }

    /// <inheritdoc/>
    public async Task UpdateTimeSheetDetailAsync(TimeSheetDetail timeSheetDetail)
    {
        timeSheetDetail = _auditingService.SetAuditedEntity(timeSheetDetail, created: false);
        await _timeSheetDetailRepository.UpdateAsync(timeSheetDetail);
        await _timeSheetDetailRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateTimeSheetDetailAsync] - TimeSheetDetail updated successfully for the {timeSheetDetail.TimeSheetDetailId}");
    }
    /// <inheritdoc/>
    public async Task<TimeSheetDetail?> GetTimeSheetDetailByIdAsync(int id)
    {
        return await _timeSheetDetailRepository.
            FirstOrDefaultAsync(x => x.TimeSheetDetailId == id);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<TimeSheetDetail>> GetAllTimeSheetDetails()
    {
        return _timeSheetDetailRepository.GetAllAsync();
    }

    /// <inheritdoc/>
    public async Task DeleteTimeSheetDetailAsync(int timeSheetDetailId)
    {
        await _timeSheetDetailRepository.DeleteAsync(x => x.TimeSheetDetailId == timeSheetDetailId);
        await _timeSheetDetailRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteTimeSheetDetailAsync] - TimeSheetDetail deleted successfully for the {timeSheetDetailId}");
    }

    /// <inheritdoc/>
    public List<TimeSheetDetail> GetTimesheetDetailByDateAsync(RequestDto timesheetDetailDto)
    {
        if (timesheetDetailDto?.EmployeeId == null)
            return _timeSheetDetailRepository.GetQueryable(filter: x => x.Timesheet.TimesheetDate >= timesheetDetailDto.FormDate && x.Timesheet.TimesheetDate <= timesheetDetailDto.ToDate && x.IsDeleted == false, include: x => x.Include(x => x.Timesheet).ThenInclude(x => x.Employee)).Include(x => x.Task).Include(x => x.Project).Include(x => x.Client).ToList();

        return _timeSheetDetailRepository.GetQueryable(filter: x => x.Timesheet.EmployeeId == timesheetDetailDto.EmployeeId && x.Timesheet.TimesheetDate >= timesheetDetailDto.FormDate && x.Timesheet.TimesheetDate <= timesheetDetailDto.ToDate && x.IsDeleted == false, include: x => x.Include(x => x.Timesheet).ThenInclude(x => x.Employee)).Include(x => x.Task).Include(x => x.Project).Include(x => x.Client).ToList();
    }

    #region Private Methods
    private async Task<Timesheet> GetOrCreateTimesheetAsync(TimeSheetDetail timeSheetDetail, long employeeId)
    {
        var existingTimesheet = await _timeSheetRepository
            .GetQueryable(x => x.EmployeeId == employeeId && x.TimesheetDate == timeSheetDetail.TimeSheetDate)
            .Include(x => x.Employee)
            .FirstOrDefaultAsync();

        if (existingTimesheet != null)
        {
            return existingTimesheet;
        }

        var newTimesheet = new Timesheet
        {
            TimesheetDate = (DateOnly)timeSheetDetail.TimeSheetDate,
            TimesheetStatus = TimeSheetStatus.Open,
            EmployeeId = employeeId,
        };

        newTimesheet = _auditingService.SetAuditedEntity(newTimesheet, created: true);
        await _timeSheetRepository.AddAsync(newTimesheet);
        await _timeSheetRepository.CompleteAsync();
        _logger.LogInformation($"[AddTimeSheetDetailAsync] - Timesheet '{newTimesheet.TimesheetDate}' added successfully by employeeId '{employeeId}'");

        return newTimesheet;
    }
    #endregion

}
