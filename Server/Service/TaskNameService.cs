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
    private ILogger<TaskNameService> _logger;

    /// <summary>
    /// Initializes a new instance 
    /// </summary>
    /// <param name="ITaskNameRepository">dfhgdj</param>
    /// <param name="ILogger<TaskNameService>">gfhk</param>
    public TaskNameService(IEmployeeService employeeService, ITaskNameRepository taskNameRepository, ILogger<TaskNameService> logger, ITimeSheetDetailRepository timeSheetDetailRepository, ITimesheetRepository timeSheetRepository)
    {
        _employeeService = employeeService;
        _taskNameRepository = taskNameRepository;
        _logger = logger;
        _timeSheetDetailRepository = timeSheetDetailRepository;
        _timeSheetRepository = timeSheetRepository;
    }

    /// <inheritdoc/>
    public async Task AddTaskNameAsync(TaskName taskName, string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            _logger.LogWarning("[AddTaskNameAsync] - Email is null or empty");
            return;
        }

        TimeSheetDetail? existingTask = await _timeSheetDetailRepository
            .GetQueryable(x => x.Timesheet.Employee.CompanyEmail == email && x.Task.TaskDetails == taskName.TaskDetails)
            .FirstOrDefaultAsync();

        if (existingTask != null)
        {
            _logger.LogInformation($"[AddTaskNameAsync] - Task '{taskName.TaskDetails}' already exists for email '{email}'");
            return;
        }

        await _taskNameRepository.AddAsync(taskName);
        await _taskNameRepository.CompleteAsync();

        var currentDate = DateOnly.FromDateTime(DateTime.Now);
        var existingTimesheet = await _timeSheetRepository
            .GetQueryable(x => x.Employee.CompanyEmail == email && x.TimesheetDate == currentDate)
            .Include(x => x.Employee)
            .FirstOrDefaultAsync();

        if (existingTimesheet == null)
        {
            var employee = await _employeeService.GetEmployeeByEmailAsync(email);
            if (employee == null)
            {
                _logger.LogWarning($"[AddTaskNameAsync] - Employee with email '{email}' not found");
                return;
            }

            Timesheet newTimesheet = new()
            {
                TimesheetDate = currentDate,
                TimesheetStatus = TimeSheetStatus.Open,
                EmployeeId = employee.EmployeeId
            };

            await _timeSheetRepository.AddAsync(newTimesheet);
            await _timeSheetRepository.CompleteAsync();

            existingTimesheet = await _timeSheetRepository
                .GetQueryable(x => x.Employee.CompanyEmail == email && x.TimesheetDate == currentDate)
                .Include(x => x.Employee)
                .FirstOrDefaultAsync();
        }

        TimeSheetDetail newTimeSheetDetail = new()
        {
            TaskId = taskName.TaskId,
            HoursSpent = 0,
            TimesheetId = existingTimesheet.TimesheetId,
            IsBillable = false,
            TaskType = TaskType.Design
        };

        await _timeSheetDetailRepository.AddAsync(newTimeSheetDetail);
        await _taskNameRepository.CompleteAsync();

        _logger.LogInformation($"[AddTaskNameAsync] - Task '{taskName.TaskDetails}' added successfully for email '{email}'");
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
