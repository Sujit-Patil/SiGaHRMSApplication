using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Interfaces;

/// <summary>
/// TaskName service interface to perform Add, Update, and delete funtionality.
/// </summary>
/// <param name="TaskName"></param>
public interface ITaskNameService
{
    /// <summary>
    /// AddTaskNameAsync method will add TaskName to Db
    /// </summary>
    /// <param name="taskName">TaskName</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task AddTaskNameAsync(TaskName taskName,string email);

    /// <summary>
    /// UpdateTaskNameAsync method perform update funtionality
    /// </summary>
    /// <param name="taskName">TaskName</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task UpdateTaskNameAsync(TaskName taskName);

    /// <summary>
    /// DeleteTaskNameAsync method perform Delete funtionality
    /// </summary>
    /// <param name="taskNameid">TaskName Id</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task DeleteTaskNameAsync(int taskNameId);

    /// <summary>
    /// GetTaskNameByIdAsync method gives TaskName using Id
    /// </summary>
    /// <param name="id">TaskName Id</param>
    /// <returns>Returns single TaskName </returns>
    public Task<TaskName?> GetTaskNameByIdAsync(int id);

    /// <summary>
    /// GetAllTaskNames gives list of TaskNames
    /// </summary>
    /// <returns>Returns list of TaskName</returns>
    public Task<IEnumerable<TaskName>> GetAllTaskNames();

}
