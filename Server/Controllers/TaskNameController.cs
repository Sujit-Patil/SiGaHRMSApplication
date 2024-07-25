using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Constants;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Validations;

namespace SiGaHRMS.ApiService.Controllers;

/// <summary>TaskNameController
/// TaskName Controller 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TaskNameController : ControllerBase
{
    private readonly ITaskNameService _taskNameService;
    private ValidationResult validationResult;

    /// <summary>
    /// Initializes a new instance of see ref<paramref name="taskNameController"/>
    /// </summary>
    /// <param name="TaskNameService"></param>
    public TaskNameController(ITaskNameService taskNameService)
    {
        _taskNameService = taskNameService;
        validationResult = new();

    }

    /// <summary>
    /// The controller method to retrive all TaskNames.
    /// </summary>
    /// <returns>returns list of TaskNames</returns>

    [HttpGet]
    public Task<IEnumerable<TaskName>> GetAllTaskNames()
    {
        return _taskNameService.GetAllTaskNames();
    }


    /// <summary>
    /// Get method to retrive single TaskName
    /// </summary>
    /// <param name="id">taskName Id</param>
    /// <returns> return single TaskName using taskName Id</returns>
    [HttpGet("{id:int}")]
    public async Task<TaskName?> GetTaskNameByIdAsync(int id)
    {
        return await _taskNameService.GetTaskNameByIdAsync(id);
    }

    /// <summary>
    /// Post method to Add TaskName to database
    /// </summary>
    /// <param name="TaskName"> TaskName object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPost]
    [Authorize]
    public async Task<ValidationResult> AddTaskNameAsync(TaskName taskName)
    {
        try
        {
            await _taskNameService.AddTaskNameAsync(taskName);
        }
        catch (Exception ex)
        {
            validationResult.AddErrorMesageCode(UserActionConstants.TaskAddFailed, UserActionConstants.ErrorDescriptions);
        }
        return validationResult;

    }

    /// <summary>
    /// Upadte method to Update TaskName to database
    /// </summary>
    /// <param name="TaskName">TaskName object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPut]
    public async Task UpdateTaskNameAsync(TaskName taskName)
    {
        await _taskNameService.UpdateTaskNameAsync(taskName);
    }

    /// <summary>
    /// Delete method to delete TaskName to database
    /// </summary>
    /// <param name="id">taskName Id</param>
    /// <returns>returns bool</returns>
    [HttpDelete("{id:int}")]
    public async Task<bool> DeleteTaskNameAsync(int id)
    {
        await _taskNameService.DeleteTaskNameAsync(id);
        return true;
    }
}
