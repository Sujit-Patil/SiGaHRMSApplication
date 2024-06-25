using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Interfaces;

/// <summary>
/// Project service interface to perform Add, Update, and delete funtionality.
/// </summary>
/// <param name="Project"></param>
public interface IProjectService
{
    /// <summary>
    /// AddProjectAsync method will add Project to Db
    /// </summary>
    /// <param name="project">Project</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task AddProjectAsync(Project project);

    /// <summary>
    /// UpdateProjectAsync method perform update funtionality
    /// </summary>
    /// <param name="project">Project</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task UpdateProjectAsync(Project project);

    /// <summary>
    /// DeleteProjectAsync method perform Delete funtionality
    /// </summary>
    /// <param name="projectid">Project Id</param>
    /// <returns>Returns asynchronous Task.</returns>
    public Task DeleteProjectAsync(int projectId);

    /// <summary>
    /// GetProjectByIdAsync method gives Project using Id
    /// </summary>
    /// <param name="id">Project Id</param>
    /// <returns>Returns single Project </returns>
    public Task<Project?> GetProjectByIdAsync(int id);

    /// <summary>
    /// GetAllProjects gives list of Projects
    /// </summary>
    /// <returns>Returns list of Project</returns>
    public List<Project> GetAllProjects();

}
