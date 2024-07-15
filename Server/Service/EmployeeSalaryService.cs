using Microsoft.EntityFrameworkCore;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Dto;

namespace SiGaHRMS.ApiService.Service;

public class EmployeeSalaryService : IEmployeeSalaryService
{
    private readonly IEmployeeSalaryRepository _employeeSalaryRepository;
    private readonly IAuditingService _auditingService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private ILogger<EmployeeSalaryService> _logger;

    /// <summary>
    /// Initializes a new instance 
    /// </summary>
    /// <param name="IEmployeeSalaryRepository">dfhgdj</param>
    /// <param name="ILogger<EmployeeSalaryService>">gfhk</param>
    public EmployeeSalaryService(IEmployeeSalaryRepository employeeSalaryRepository, ILogger<EmployeeSalaryService> logger, IAuditingService auditingService, IDateTimeProvider dateTimeProvider)
    {
        _employeeSalaryRepository = employeeSalaryRepository;
        _auditingService = auditingService;
        _logger = logger;
        _dateTimeProvider = dateTimeProvider;
    }

    /// <inheritdoc/>
    public async Task AddEmployeeSalaryAsync(EmployeeSalary employeeSalary)
    {
        employeeSalary.SalaryForAMonth = _dateTimeProvider.Now;
        employeeSalary = _auditingService.SetAuditedEntity(employeeSalary, true);
        await _employeeSalaryRepository.AddAsync(employeeSalary);
        await _employeeSalaryRepository.CompleteAsync();
        _logger.LogInformation($"[AddEmployeeSalaryAsyns] - {employeeSalary.EmployeeSalaryId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateEmployeeSalaryAsync(EmployeeSalary employeeSalary)
    {
        await _employeeSalaryRepository.UpdateAsync(employeeSalary);
        await _employeeSalaryRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateEmployeeSalaryAsyns] - EmployeeSalary updated successfully for the {employeeSalary.EmployeeSalaryId}");
    }

    /// <inheritdoc/>
    public async Task<EmployeeSalary?> GetEmployeeSalaryByIdAsync(int id)
    {
        return await _employeeSalaryRepository.
            FirstOrDefaultAsync(x => x.EmployeeSalaryId == id);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<EmployeeSalary>> GetAllEmployeeSalarys()
    {
        return _employeeSalaryRepository.GetAllAsync("Employee");
    }

    /// <inheritdoc/>
    public async Task DeleteEmployeeSalaryAsync(int employeeSalaryId)
    {
        await _employeeSalaryRepository.DeleteAsync(x => x.EmployeeSalaryId == employeeSalaryId);
        await _employeeSalaryRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteEmployeeSalaryAsync] - EmployeeSalary deleted successfully for the {employeeSalaryId}");
    }

    public List<EmployeeSalary> GetEmployeeSalaryByDateAsync(RequestDto employeeSalaryDto)
    {
        if (employeeSalaryDto?.EmployeeId == null)
            return _employeeSalaryRepository.GetQueryable(filter: x => DateOnly.FromDateTime(x.SalaryForAMonth) >= employeeSalaryDto.FormDate && DateOnly.FromDateTime(x.SalaryForAMonth) <= employeeSalaryDto.ToDate && x.IsDeleted == false, include: x => x.Include(x => x.Employee)).ToList();

        return _employeeSalaryRepository.GetQueryable(filter: x => x.EmployeeId == employeeSalaryDto.EmployeeId && DateOnly.FromDateTime(x.SalaryForAMonth) >= employeeSalaryDto.FormDate && DateOnly.FromDateTime(x.SalaryForAMonth) <= employeeSalaryDto.ToDate && x.IsDeleted == false, include: x => x.Include(x => x.Employee)).ToList();
    }

}
