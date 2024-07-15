using Microsoft.EntityFrameworkCore;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Enum;
using SiGaHRMS.Data.Repository;

namespace SiGaHRMS.ApiService.Service;

public class TaskNameService : ITaskNameService
{
    private readonly ITaskNameRepository _taskNameRepository;
    private readonly ITimeSheetDetailRepository _timeSheetDetailRepository;
    private readonly ITimesheetRepository _timeSheetRepository;
    private readonly IEmployeeService _employeeService;
    private readonly ISessionService _sessionService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IAuditingService _auditingService;
    private ILogger<TaskNameService> _logger;

    /// <summary>
    /// Initializes a new instance 
    /// </summary>
    /// <param name="ITaskNameRepository">dfhgdj</param>
    /// <param name="ILogger<TaskNameService>">gfhk</param>
    public TaskNameService(IEmployeeService employeeService, IAuditingService auditingService, IDateTimeProvider dateTimeProvider, ITaskNameRepository taskNameRepository, ISessionService sessionService, ILogger<TaskNameService> logger, ITimeSheetDetailRepository timeSheetDetailRepository, ITimesheetRepository timeSheetRepository)
    {
        _employeeService = employeeService;
        _dateTimeProvider = dateTimeProvider;
        _auditingService = auditingService;
        _taskNameRepository = taskNameRepository;
        _sessionService = sessionService;
        _logger = logger;
        _timeSheetDetailRepository = timeSheetDetailRepository;
        _timeSheetRepository = timeSheetRepository;
    }

    /// <inheritdoc/>
    public async Task AddTaskNameAsync(TaskName taskName)
    {
        var employeeId = _sessionService.GetCurrentEmployeeId();
        var currentDate = _dateTimeProvider.Today;
        var existingTask = await _taskNameRepository.GetAsync(x => x.TaskDetails == taskName.TaskDetails);

        if (existingTask == null)
        {
            await _taskNameRepository.AddAsync(taskName);
            await _taskNameRepository.CompleteAsync();
            existingTask = taskName;
            _logger.LogInformation($"[AddTaskNameAsync] - TaskName '{taskName.TaskId}' added successfully by employeeId '{employeeId}'");
        }
        
        await GetOrCreateTimesheetAsync(employeeId, currentDate, existingTask.TaskId);
    }

    /// <inheritdoc/>
    public async Task UpdateTaskNameAsync(TaskName taskName)
    {
        await _taskNameRepository.UpdateAsync(taskName);
        await _taskNameRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateTaskNameAsyns] - TaskName updated successfully for the {taskName.TaskId}");
    }

    /// <inheritdoc/>
    public async Task<TaskName?> GetTaskNameByIdAsync(int id)
    {
        return await _taskNameRepository.
            FirstOrDefaultAsync(x => x.TaskId == id);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<TaskName>> GetAllTaskNames()
    {
        return _taskNameRepository.GetAllAsync();
    }

    /// <inheritdoc/>
    public async Task DeleteTaskNameAsync(int taskNameId)
    {
        await _taskNameRepository.DeleteAsync(x => x.TaskId == taskNameId);
        await _taskNameRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteTaskNameAsync] - TaskName deleted successfully for the {taskNameId}");
    }

    #region Private methods
    private async Task GetOrCreateTimesheetAsync(long employeeId, DateOnly currentDate,int taskId)
    {
        var existingTimesheet = await _timeSheetRepository
            .GetQueryable(x => x.EmployeeId == employeeId && x.TimesheetDate == currentDate)
            .Include(x => x.Employee)
            .FirstOrDefaultAsync();

        if (existingTimesheet == null)
        {
            existingTimesheet = new Timesheet
            {
                TimesheetDate = currentDate,
                TimesheetStatus = TimeSheetStatus.Open,
                EmployeeId = employeeId,
               
            };
            existingTimesheet=_auditingService.SetAuditedEntity(existingTimesheet, true);
            await _timeSheetRepository.AddAsync(existingTimesheet);
            await _timeSheetRepository.CompleteAsync();
            _logger.LogInformation($"[AddTaskNameAsync] - Timesheet '{existingTimesheet.TimesheetDate}' added successfully by employeeId '{employeeId}'");

            
        }

        var newTimeSheetDetail = new TimeSheetDetail
        {
            TaskId = taskId,
            HoursSpent = 0,
            TimesheetId = existingTimesheet.TimesheetId,
            IsBillable = false,
            TaskType = TaskType.Design,
        };
        newTimeSheetDetail = _auditingService.SetAuditedEntity(newTimeSheetDetail, true);
        await _timeSheetDetailRepository.AddAsync(newTimeSheetDetail);
        await _timeSheetDetailRepository.CompleteAsync();
        _logger.LogInformation($"[AddTaskNameAsync] - TimeSheetDetail for '{currentDate}' added successfully by employeeId '{employeeId}'");
    }

    #endregion
}
