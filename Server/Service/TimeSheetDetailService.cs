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
    private readonly ISessionService _sessionService;
    private readonly IAuditingService _auditingService;
    private ILogger<TimeSheetDetailService> _logger;

    /// <summary>
    /// Initializes a new instance 
    /// </summary>
    /// <param name="ITimeSheetDetailRepository">dfhgdj</param>
    /// <param name="ILogger<TimeSheetDetailService>">gfhk</param>
    public TimeSheetDetailService(ISessionService sessionService, IAuditingService auditingService, ITimeSheetDetailRepository timeSheetDetailRepository, ITimesheetRepository timeSheetRepository, ILogger<TimeSheetDetailService> logger)
    {
        _timeSheetDetailRepository = timeSheetDetailRepository;
        _sessionService = sessionService;
        _auditingService = auditingService;
        _timeSheetRepository = timeSheetRepository;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task AddTimeSheetDetailAsync(TimeSheetDetail timeSheetDetail)
    {
        if (timeSheetDetail?.TimesheetId == null)
        {
            Timesheet newTimesheet = new()
            {
                TimesheetDate = DateOnly.FromDateTime((DateTime)(timeSheetDetail.CreatedDateTime)),
                TimesheetStatus = TimeSheetStatus.Open,
                EmployeeId = _sessionService.GetCurrentEmployeeId(),
            };
            var newTimesheetWithAudit=_auditingService.SetAuditedEntity(newTimesheet);
            await _timeSheetRepository.AddAsync(newTimesheetWithAudit);
            await _timeSheetRepository.CompleteAsync();

            timeSheetDetail.TimesheetId = (await _timeSheetRepository.GetQueryable(x => x.EmployeeId == newTimesheet.EmployeeId && x.TimesheetDate == newTimesheet.TimesheetDate).FirstOrDefaultAsync())?.TimesheetId;

        }

        await _timeSheetDetailRepository.AddAsync(timeSheetDetail);
        await _timeSheetDetailRepository.CompleteAsync();
        _logger.LogInformation($"[AddTimeSheetDetailAsyns] - {timeSheetDetail.TimeSheetDetailId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateTimeSheetDetailAsync(TimeSheetDetail timeSheetDetail)
    {
        await _timeSheetDetailRepository.UpdateAsync(timeSheetDetail);
        await _timeSheetDetailRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateTimeSheetDetailAsyns] - TimeSheetDetail updated successfully for the {timeSheetDetail.TimeSheetDetailId}");
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

    public List<TimeSheetDetail> GetTimesheetDetailByDateAsync(RequestDto timesheetDetailDto)
    {
        if (timesheetDetailDto?.EmployeeId == null)
            return _timeSheetDetailRepository.GetQueryable(filter: x => x.Timesheet.TimesheetDate >= timesheetDetailDto.FormDate && x.Timesheet.TimesheetDate <= timesheetDetailDto.ToDate && x.IsDeleted == false, include: x => x.Include(x => x.Timesheet).ThenInclude(x=>x.Employee)).Include(x => x.Task).Include(x => x.Project).Include(x => x.Client).ToList();

        return _timeSheetDetailRepository.GetQueryable(filter: x => x.Timesheet.EmployeeId == timesheetDetailDto.EmployeeId && x.Timesheet.TimesheetDate >= timesheetDetailDto.FormDate && x.Timesheet.TimesheetDate <= timesheetDetailDto.ToDate && x.IsDeleted == false, include: x => x.Include(x => x.Timesheet).ThenInclude(x => x.Employee)).Include(x => x.Task).Include(x => x.Project).Include(x => x.Client).ToList();
    }

    private TimeSheetDetail SetAuditedEntity(TimeSheetDetail timeSheetDetail)
    {
        if (timeSheetDetail.CreatedDateTime == null)
            timeSheetDetail.CreatedDateTime = DateTime.UtcNow;
        timeSheetDetail.CreatedBy = _sessionService.GetCurrentEmployeeId();

        if (timeSheetDetail.IsDeleted == true)
            timeSheetDetail.DeletedDateTime = DateTime.UtcNow;
        timeSheetDetail.CreatedBy = _sessionService.GetCurrentEmployeeId();

        timeSheetDetail.DeletedDateTime = DateTime.UtcNow;
        timeSheetDetail.CreatedBy = _sessionService.GetCurrentEmployeeId();

        return timeSheetDetail;
    }

}
