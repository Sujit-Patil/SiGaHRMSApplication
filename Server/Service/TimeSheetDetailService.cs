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
    private readonly IHolidayRepository _holidayRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ISessionService _sessionService;
    private readonly IAuditingService _auditingService;
    private readonly ILogger<TimeSheetDetailService> _logger;

    public TimeSheetDetailService(
        ISessionService sessionService,
        IAuditingService auditingService,
        IHolidayRepository holidayRepository,
        ITimeSheetDetailRepository timeSheetDetailRepository,
        IDateTimeProvider dateTimeProvider,
        ITimesheetRepository timeSheetRepository,
        ILogger<TimeSheetDetailService> logger)
    {
        _timeSheetDetailRepository = timeSheetDetailRepository;
        _sessionService = sessionService;
        _holidayRepository = holidayRepository;
        _auditingService = auditingService;
        _dateTimeProvider = dateTimeProvider;
        _timeSheetRepository = timeSheetRepository;
        _logger = logger;
    }

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

    public async Task UpdateTimeSheetDetailAsync(TimeSheetDetail timeSheetDetail)
    {
        timeSheetDetail = _auditingService.SetAuditedEntity(timeSheetDetail, created: false);
        await _timeSheetDetailRepository.UpdateAsync(timeSheetDetail);
        await _timeSheetDetailRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateTimeSheetDetailAsync] - TimeSheetDetail updated successfully for the {timeSheetDetail.TimeSheetDetailId}");
    }

    public async Task<TimeSheetDetail?> GetTimeSheetDetailByIdAsync(int id)
    {
        return await _timeSheetDetailRepository.FirstOrDefaultAsync(x => x.TimeSheetDetailId == id);
    }

    public Task<IEnumerable<TimeSheetDetail>> GetAllTimeSheetDetails()
    {
        return _timeSheetDetailRepository.GetAllAsync();
    }

    public async Task DeleteTimeSheetDetailAsync(int timeSheetDetailId)
    {
        await _timeSheetDetailRepository.DeleteAsync(x => x.TimeSheetDetailId == timeSheetDetailId);
        await _timeSheetDetailRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteTimeSheetDetailAsync] - TimeSheetDetail deleted successfully for the {timeSheetDetailId}");
    }

    public List<TimeSheetDetail> GetTimesheetDetailByDateAsync(RequestDto timesheetDetailDto)
    {
        var query = _timeSheetDetailRepository.GetQueryable(x => x.IsDeleted == false);

        if (timesheetDetailDto?.EmployeeId != null)
        {
            query = query.Where(x => x.Timesheet.EmployeeId == timesheetDetailDto.EmployeeId);
        }

        if (timesheetDetailDto?.FormDate != null && timesheetDetailDto?.ToDate != null)
        {
            query = query.Where(x => x.Timesheet.TimesheetDate >= timesheetDetailDto.FormDate && x.Timesheet.TimesheetDate <= timesheetDetailDto.ToDate);
        }

        return query.Include(x => x.Timesheet).ThenInclude(x => x.Employee)
                    .Include(x => x.Task).ThenInclude(x=>x.Project)
                    .ToList();
    }

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
}
