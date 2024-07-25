using Microsoft.EntityFrameworkCore;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Service;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IAuditingService _auditingService;
    private ILogger<ProjectService> _logger;

    /// <summary>
    /// Initializes a new instance 
    /// </summary>
    /// <param name="IProjectRepository">dfhgdj</param>
    /// <param name="ILogger<ProjectService>">gfhk</param>
    public ProjectService(IProjectRepository projectRepository, ILogger<ProjectService> logger, IAuditingService auditingService)
    {
        _projectRepository = projectRepository;
        _logger = logger;
        _auditingService = auditingService;
    }

    /// <inheritdoc/>
    public async Task AddProjectAsync(Project project)
    {
        try
        {
            project = _auditingService.SetAuditedEntity(project, true);
            await _projectRepository.AddAsync(project);
            await _projectRepository.CompleteAsync();
            _logger.LogInformation($"[AddProjectAsyns] - {project.ProjectId} added successfully");
        }
        catch (Exception ex) { }
       
    }

    /// <inheritdoc/>
    public async Task UpdateProjectAsync(Project project)
    {
        project = _auditingService.SetAuditedEntity(project, false);
        await _projectRepository.UpdateAsync(project);
        await _projectRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateProjectAsyns] - Project updated successfully for the {project.ProjectId}");
    }

    /// <inheritdoc/>
    public async Task<Project?> GetProjectByIdAsync(int id)
    {
        return await _projectRepository.
            FirstOrDefaultAsync(x => x.ProjectId == id);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Project>> GetAllProjects()
    {
        return await _projectRepository.GetQueryable(x => x.IsDeleted == false, include: x => x.Include(x => x.BillingPlatform)).Include(x=>x.Client).ToListAsync();

    }

    /// <inheritdoc/>
    public async Task DeleteProjectAsync(int projectId)
    {
        await _projectRepository.DeleteAsync(x => x.ProjectId == projectId);
        await _projectRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteProjectAsync] - Project deleted successfully for the {projectId}");
    }

}
