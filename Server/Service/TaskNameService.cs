using Microsoft.EntityFrameworkCore;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Enum;

namespace SiGaHRMS.ApiService.Service;

public class TaskNameService : ITaskNameService
{
    private readonly ITaskNameRepository _taskNameRepository;
    private readonly ITimeSheetDetailRepository _timeSheetDetailRepository;
    private readonly ITimesheetRepository _timeSheetRepository;
    private readonly IEmployeeService _employeeService;
    private readonly ISessionService _sessionService;
    private ILogger<TaskNameService> _logger;

    /// <summary>
    /// Initializes a new instance 
    /// </summary>
    /// <param name="ITaskNameRepository">dfhgdj</param>
    /// <param name="ILogger<TaskNameService>">gfhk</param>
    public TaskNameService(IEmployeeService employeeService, ITaskNameRepository taskNameRepository, ISessionService sessionService, ILogger<TaskNameService> logger, ITimeSheetDetailRepository timeSheetDetailRepository, ITimesheetRepository timeSheetRepository)
    {
        _employeeService = employeeService;
        _taskNameRepository = taskNameRepository;
        _sessionService = sessionService;
        _logger = logger;
        _timeSheetDetailRepository = timeSheetDetailRepository;
        _timeSheetRepository = timeSheetRepository;
    }

    /// <inheritdoc/>
    public async Task AddTaskNameAsync(TaskName taskName)
    {
        await _taskNameRepository.AddAsync(taskName);
        await _taskNameRepository.CompleteAsync();

        var currentDate = DateOnly.FromDateTime(DateTime.Now);
        var employeeId = _sessionService.GetCurrentEmployeeId();
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
                EmployeeId = employeeId
            };

            await _timeSheetRepository.AddAsync(existingTimesheet);
            await _timeSheetRepository.CompleteAsync();
        }

        var newTimeSheetDetail = new TimeSheetDetail
        {
            TaskId = taskName.TaskId,
            HoursSpent = 0,
            TimesheetId = existingTimesheet.TimesheetId,
            IsBillable = false,
            TaskType = TaskType.Design
        };

        await _timeSheetDetailRepository.AddAsync(newTimeSheetDetail);
        await _timeSheetDetailRepository.CompleteAsync();

        _logger.LogInformation($"[AddTaskNameAsync] - Task '{taskName.TaskDetails}' added successfully for ememployeeIdail '{employeeId}'");
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

}
