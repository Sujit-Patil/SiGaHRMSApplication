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
    private readonly ILogger<EmployeeSalaryStructureService> _logger;

    /// <summary>
    /// Initializes a new instance of the EmployeeSalaryStructureService class.
    /// </summary>
    /// <param name="employeeSalaryStructureRepository">The repository for managing employee salary structure data.</param>
    /// <param name="auditingService">The service for auditing operations.</param>
    /// <param name="logger">The logger for logging messages related to EmployeeSalaryStructureService.</param>
    public EmployeeSalaryStructureService(
        IEmployeeSalaryStructureRepository employeeSalaryStructureRepository,
        IAuditingService auditingService,
        ILogger<EmployeeSalaryStructureService> logger)
    {
        _employeeSalaryStructureRepository = employeeSalaryStructureRepository;
        _auditingService = auditingService;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task AddEmployeeSalaryStructureAsync(EmployeeSalaryStructure employeeSalaryStructure)
    {
        var existingStructure = await GetExistingEmployeeSalaryStructure(employeeSalaryStructure.EmployeeId);
        if (existingStructure != null)
        {
            UpdateExistingSalaryStructureEndDate(existingStructure, employeeSalaryStructure.FromDate);
        }

        employeeSalaryStructure.ToDate = null;
        employeeSalaryStructure = _auditingService.SetAuditedEntity(employeeSalaryStructure, true);
        await _employeeSalaryStructureRepository.AddAsync(employeeSalaryStructure);
        await _employeeSalaryStructureRepository.CompleteAsync();
        _logger.LogInformation($"[AddEmployeeSalaryStructureAsync] - {employeeSalaryStructure.EmployeeSalaryStructureId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateEmployeeSalaryStructureAsync(EmployeeSalaryStructure employeeSalaryStructure)
    {
        var existingStructure = await GetExistingEmployeeSalaryStructure(employeeSalaryStructure.EmployeeId, employeeSalaryStructure.EmployeeSalaryStructureId);
        if (existingStructure == null) return;

        if (existingStructure.FromDate != employeeSalaryStructure.FromDate)
        {
            var previousStructure = await GetPreviousEmployeeSalaryStructure(employeeSalaryStructure.EmployeeId, existingStructure.FromDate);
            if (previousStructure != null)
            {
                UpdateExistingSalaryStructureEndDate(previousStructure, employeeSalaryStructure.FromDate);
            }
        }

        UpdateEmployeeSalaryStructure(existingStructure, employeeSalaryStructure);
        employeeSalaryStructure = _auditingService.SetAuditedEntity(employeeSalaryStructure, false);
        await _employeeSalaryStructureRepository.UpdateAsync(existingStructure);
        await _employeeSalaryStructureRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateEmployeeSalaryStructureAsync] - EmployeeSalaryStructure updated successfully for {existingStructure.EmployeeSalaryStructureId}");
    }

    /// <inheritdoc/>
    public Task<EmployeeSalaryStructure?> GetEmployeeSalaryStructureByIdAsync(int id) =>
        _employeeSalaryStructureRepository.FirstOrDefaultAsync(x => x.EmployeeSalaryStructureId == id);

    /// <inheritdoc/>
    public Task<IEnumerable<EmployeeSalaryStructure>> GetAllEmployeeSalaryStructures() =>
        _employeeSalaryStructureRepository.GetAllAsync("Employee");

    /// <inheritdoc/>
    public async Task DeleteEmployeeSalaryStructureAsync(int employeeSalaryStructureId)
    {
        await _employeeSalaryStructureRepository.DeleteAsync(x => x.EmployeeSalaryStructureId == employeeSalaryStructureId);
        await _employeeSalaryStructureRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteEmployeeSalaryStructureAsync] - EmployeeSalaryStructure deleted successfully for {employeeSalaryStructureId}");
    }

    /// <inheritdoc/>
    public List<EmployeeSalaryStructure> GetEmployeeSalaryStructuresByDateAsync(RequestDto employeeSalaryStructureDto)
    {
        var query = _employeeSalaryStructureRepository.GetQueryable(
            x => x.IsDeleted == false,
            include: x => x.Include(x => x.Employee));

        if (employeeSalaryStructureDto?.EmployeeId != null)
        {
            query = query.Where(x => x.EmployeeId == employeeSalaryStructureDto.EmployeeId && x.ToDate == null);
        }

        if (employeeSalaryStructureDto?.FormDate != null)
        {
            query = query.Where(x => x.FromDate >= employeeSalaryStructureDto.FormDate);
        }

        if (employeeSalaryStructureDto?.ToDate != null)
        {
            query = query.Where(x => x.ToDate <= employeeSalaryStructureDto.ToDate);
        }

        if (employeeSalaryStructureDto?.EmployeeId == null && employeeSalaryStructureDto?.FormDate == null && employeeSalaryStructureDto?.ToDate == null)
        {
            query = query.Where(x => x.ToDate == null);
        }

        return query.ToList();
    }

    #region Private Methods
    private async Task<EmployeeSalaryStructure?> GetExistingEmployeeSalaryStructure(long employeeId, long? employeeSalaryStructureId = null) =>
        await _employeeSalaryStructureRepository
            .GetQueryable(x => x.EmployeeId == employeeId && x.ToDate == null && (employeeSalaryStructureId == null || x.EmployeeSalaryStructureId == employeeSalaryStructureId))
            .FirstOrDefaultAsync();

    private async Task<EmployeeSalaryStructure?> GetPreviousEmployeeSalaryStructure(long employeeId, DateOnly toDate) =>
        await _employeeSalaryStructureRepository
            .GetQueryable(x => x.EmployeeId == employeeId && x.ToDate == toDate)
            .FirstOrDefaultAsync();

    private async void UpdateExistingSalaryStructureEndDate(EmployeeSalaryStructure structure, DateOnly endDate)
    {
        structure.ToDate = endDate;
        structure = _auditingService.SetAuditedEntity(structure, false);
        await _employeeSalaryStructureRepository.UpdateAsync(structure);
    }

    private void UpdateEmployeeSalaryStructure(EmployeeSalaryStructure existing, EmployeeSalaryStructure updated)
    {
        existing.Conveyance = updated.Conveyance;
        existing.Basic = updated.Basic;
        existing.DA = updated.DA;
        existing.HRA = updated.HRA;
        existing.MedicalAllowance = updated.MedicalAllowance;
        existing.SpecialAllowance = updated.SpecialAllowance;
        existing.FromDate = updated.FromDate;
        existing.TDS = updated.TDS;
        existing.ToDate = null;
    }

    #endregion
}
