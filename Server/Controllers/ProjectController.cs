using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.ApiService.Service;
using SiGaHRMS.Data.Constants;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Validations;

namespace SiGaHRMS.ApiService.Controllers;

/// <summary>ProjectController
/// Project Controller 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;
    private ILogger<ProjectController> _logger;
    private ValidationResult validationResult;

    /// <summary>
    /// Initializes a new instance of see ref<paramref name="ProjectController"/>
    /// </summary>
    /// <param name="projectService"></param>
    public ProjectController(IProjectService projectService, ILogger<ProjectController> logger)
    {
        _projectService = projectService;
        validationResult=new ValidationResult();
        _logger = logger;
    }

    /// <summary>
    /// The controller method to retrive all Projects.
    /// </summary>
    /// <returns>returns list of Projects</returns>

    [HttpGet]
    public Task<IEnumerable<Project>> GetAllProjects()
    {
        return _projectService.GetAllProjects();
    }

    /// <summary>
    /// Get method to retrive single Project
    /// </summary>
    /// <param name="id">Project Id</param>
    /// <returns> return single Project using Project Id</returns>
    [HttpGet("{id:int}")]
    public async Task<Project?> GetProjectByIdAsync(int id)
    {
        return await _projectService.GetProjectByIdAsync(id);
    }

    /// <summary>
    /// Post method to Add Project to database
    /// </summary>
    /// <param name="project"> Project object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPost]
    public async Task<IActionResult> AddProjectAsync(Project project)
    {
        try
        {
            await _projectService.AddProjectAsync(project);

            return Ok(validationResult);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[AddProjectAsync] Error Occurs : {ex.Message}");
            validationResult.AddErrorMesageCode(UserActionConstants.UnExpectedException, UserActionConstants.ErrorDescriptions);
            return BadRequest(validationResult);
        }
    }

    /// <summary>
    /// Upadte method to Update Project to database
    /// </summary>
    /// <param name="project">Project object</param>
    /// <returns>Returns asynchronous Task.</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateProjectAsync(Project project)
    {
        try
        {
            await _projectService.UpdateProjectAsync(project);

            return Ok(validationResult);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[UpdateProjectAsync] Error Occurs : {ex.Message}");
            validationResult.AddErrorMesageCode(UserActionConstants.UnExpectedException, UserActionConstants.ErrorDescriptions);
            return BadRequest(validationResult);
        }
    }

    /// <summary>
    /// Delete method to delete Project to database
    /// </summary>
    /// <param name="id">Project Id</param>
    /// <returns>returns bool</returns>
    [HttpDelete("{id:int}")]
    public async Task<bool> DeleteProjectAsync(int id)
    {
        await _projectService.DeleteProjectAsync(id);
        return true;
    }
}
