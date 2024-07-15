using Microsoft.EntityFrameworkCore;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Dto;

namespace SiGaHRMS.ApiService.Service;

public class EmployeeSalaryStructureService : IEmployeeSalaryStructureService
{
    private readonly IEmployeeSalaryStructureRepository _employeeSalaryStructureRepository;
    private readonly IAuditingService _auditingService;
    private ILogger<EmployeeSalaryStructureService> _logger;

    /// <summary>
    /// Initializes a new instance 
    /// </summary>
    /// <param name="IEmployeeSalaryStructureRepository">dfhgdj</param>
    /// <param name="ILogger<EmployeeSalaryStructureService>">gfhk</param>
    public EmployeeSalaryStructureService(IEmployeeSalaryStructureRepository employeeSalaryStructureRepository, IAuditingService auditingService, ILogger<EmployeeSalaryStructureService> logger)
    {
        _employeeSalaryStructureRepository = employeeSalaryStructureRepository;
        _auditingService = auditingService;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task AddEmployeeSalaryStructureAsync(EmployeeSalaryStructure employeeSalaryStructure)
    {
        employeeSalaryStructure = _auditingService.SetAuditedEntity(employeeSalaryStructure, true);
        await _employeeSalaryStructureRepository.AddAsync(employeeSalaryStructure);
        await _employeeSalaryStructureRepository.CompleteAsync();
        _logger.LogInformation($"[AddEmployeeSalaryStructureAsyns] - {employeeSalaryStructure.EmployeeSalaryStructureId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateEmployeeSalaryStructureAsync(EmployeeSalaryStructure employeeSalaryStructure)
    {
        await _employeeSalaryStructureRepository.UpdateAsync(employeeSalaryStructure);
        await _employeeSalaryStructureRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateEmployeeSalaryStructureAsyns] - EmployeeSalaryStructure updated successfully for the {employeeSalaryStructure.EmployeeSalaryStructureId}");
    }

    /// <inheritdoc/>
    public async Task<EmployeeSalaryStructure?> GetEmployeeSalaryStructureByIdAsync(int id)
    {
        return await _employeeSalaryStructureRepository.
            FirstOrDefaultAsync(x => x.EmployeeSalaryStructureId == id);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<EmployeeSalaryStructure>> GetAllEmployeeSalaryStructures()
    {
        return _employeeSalaryStructureRepository.GetAllAsync("Employee");

    }

    /// <inheritdoc/>
    public async Task DeleteEmployeeSalaryStructureAsync(int employeeSalaryStructureId)
    {
        await _employeeSalaryStructureRepository.DeleteAsync(x => x.EmployeeSalaryStructureId == employeeSalaryStructureId);
        await _employeeSalaryStructureRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteEmployeeSalaryStructureAsync] - EmployeeSalaryStructure deleted successfully for the {employeeSalaryStructureId}");
    }

    public List<EmployeeSalaryStructure> GetEmployeeSalaryStructuresByDateAsync(RequestDto employeeSalaryStructureDto)
    {
        if (employeeSalaryStructureDto?.EmployeeId == null && employeeSalaryStructureDto.ToDate==null)
            return _employeeSalaryStructureRepository.GetQueryable(filter: x => x.ToDate == null && x.IsDeleted == false, include: x => x.Include(x => x.Employee)).ToList();
        
        if (employeeSalaryStructureDto?.EmployeeId == null)
            return _employeeSalaryStructureRepository.GetQueryable(filter: x => x.FromDate >= employeeSalaryStructureDto.FormDate && x.ToDate <= employeeSalaryStructureDto.ToDate && x.IsDeleted == false, include: x => x.Include(x => x.Employee)).ToList();

        return _employeeSalaryStructureRepository.GetQueryable(filter: x => x.EmployeeId == employeeSalaryStructureDto.EmployeeId && x.FromDate >= employeeSalaryStructureDto.FormDate && x.ToDate <= employeeSalaryStructureDto.ToDate && x.IsDeleted == false, include: x => x.Include(x => x.Employee)).ToList();
    }

}
