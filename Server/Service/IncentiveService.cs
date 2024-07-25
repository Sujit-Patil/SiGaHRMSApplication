using Microsoft.EntityFrameworkCore;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Dto;
using SiGaHRMS.Data.Repository;

namespace SiGaHRMS.ApiService.Service;

public class IncentiveService : IIncentiveService
{
    private readonly IIncentiveRepository _incentiveRepository;
    private readonly IAuditingService _auditingService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private ILogger<IncentiveService> _logger;

    /// <summary>
    /// Initializes a new instance 
    /// </summary>
    /// <param name="IIncentiveRepository">dfhgdj</param>
    /// <param name="ILogger<IncentiveService>">gfhk</param>
    public IncentiveService(IIncentiveRepository incentiveRepository, ILogger<IncentiveService> logger, IAuditingService auditingService, IDateTimeProvider dateTimeProvider)
    {
        _incentiveRepository = incentiveRepository;
        _logger = logger;
        _auditingService = auditingService;
        _dateTimeProvider = dateTimeProvider;
    }

    /// <inheritdoc/>
    public async Task AddIncentiveAsync(Incentive incentive)
    {
        incentive = _auditingService.SetAuditedEntity(incentive, true);
        await _incentiveRepository.AddAsync(incentive);
        await _incentiveRepository.CompleteAsync();
        _logger.LogInformation($"[AddIncentiveAsyns] - {incentive.IncentiveId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateIncentiveAsync(Incentive incentive)
    {
        incentive = _auditingService.SetAuditedEntity(incentive, false);
        await _incentiveRepository.UpdateAsync(incentive);
        await _incentiveRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateIncentiveAsyns] - Incentive updated successfully for the {incentive.IncentiveId}");
    }

    /// <inheritdoc/>
    public async Task<Incentive?> GetIncentiveByIdAsync(int id)
    {
        return await _incentiveRepository.
            FirstOrDefaultAsync(x => x.IncentiveId == id);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Incentive>> GetAllIncentives()
    {
        return await _incentiveRepository.GetQueryable(x => x.IsDeleted == false).ToListAsync();
    }

    /// <inheritdoc/>
    public async Task DeleteIncentiveAsync(int incentiveId)
    {
        await _incentiveRepository.DeleteAsync(x => x.IncentiveId == incentiveId);
        await _incentiveRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteIncentiveAsync] - Incentive deleted successfully for the {incentiveId}");
    }

    public async Task<List<Incentive>> GetIncentivesByDateAsync(RequestDto incentiveRequestDto)
    {
        var query = _incentiveRepository.GetQueryable(
            x => x.IsDeleted == false, y => y.Include(x => x.Employee));

        if (incentiveRequestDto?.EmployeeId != null)
        {
            query = query.Where(x => x.EmployeeId >= incentiveRequestDto.EmployeeId);
        }

        if (incentiveRequestDto?.FormDate != null)
        {
            query = query.Where(x => x.CreatedDateTime >= _dateTimeProvider.CastDateOnlyToDateTime((DateOnly)incentiveRequestDto.FormDate));
        }

        if (incentiveRequestDto?.ToDate != null)
        {
            query = query.Where(x => x.CreatedDateTime <= _dateTimeProvider.CastDateOnlyToDateTime((DateOnly)incentiveRequestDto.ToDate));
        }

        return await query.Include(n => n.IncentivePurpose).ToListAsync();
    }

}
