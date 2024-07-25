using Microsoft.EntityFrameworkCore;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Dto;
using SiGaHRMS.Data.Repository;

namespace SiGaHRMS.ApiService.Service;

public class HolidayService : IHolidayService
{
    private readonly IHolidayRepository _holidayRepository;
    private readonly IAuditingService _auditingService;
    private ILogger<HolidayService> _logger;

    /// <summary>
    /// Initializes a new instance 
    /// </summary>
    /// <param name="IHolidayRepository">dfhgdj</param>
    /// <param name="ILogger<HolidayService>">gfhk</param>
    public HolidayService(IHolidayRepository holidayRepository, IAuditingService auditingService, ILogger<HolidayService> logger)
    {
        _holidayRepository = holidayRepository;
        _auditingService = auditingService;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task AddHolidayAsync(Holiday holiday)
    {
        var res = await _holidayRepository.GetQueryable(x => x.Date == holiday.Date).FirstOrDefaultAsync();

        if (res != null)
        {
            return;
        }

        holiday = _auditingService.SetAuditedEntity(holiday, true);

        await _holidayRepository.AddAsync(holiday);
        await _holidayRepository.CompleteAsync();

        _logger.LogInformation($"[AddHolidayAsyns] - {holiday.HolidayId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateHolidayAsync(Holiday holiday)
    {
        var res = await _holidayRepository.GetQueryable(x => x.HolidayId == holiday.HolidayId).FirstOrDefaultAsync();

        if (res == null)
        {
            return;
        }

        res.Date = holiday.Date;
        res.Description = holiday.Description;
        res.IsDeleted = holiday.IsDeleted;
        res = _auditingService.SetAuditedEntity(res, false);

        await _holidayRepository.UpdateAsync(res);
        await _holidayRepository.CompleteAsync();

        _logger.LogInformation($"[UpdateHolidayAsyns] - Holiday updated successfully for the {holiday.HolidayId}");
    }

    /// <inheritdoc/>
    public async Task<Holiday?> GetHolidayByIdAsync(int id)
    {
        return await _holidayRepository.
            FirstOrDefaultAsync(x => x.HolidayId == id);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<Holiday>> GetAllHolidays()
    {
        return _holidayRepository.GetAllAsync();

    }

    /// <inheritdoc/>
    public async Task DeleteHolidayAsync(int holidayId)
    {
        await _holidayRepository.DeleteAsync(x => x.HolidayId == holidayId);
        await _holidayRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteHolidayAsync] - Holiday deleted successfully for the {holidayId}");
    }

    public List<Holiday> GetHolidaysByDateAsync(RequestDto holidayRequestDto)
    {
        var query = _holidayRepository.GetQueryable(
            x => x.IsDeleted == false);
        if (holidayRequestDto?.FormDate != null)
        {
            query = query.Where(x => x.Date >= holidayRequestDto.FormDate);
        }

        if (holidayRequestDto?.ToDate != null)
        {
            query = query.Where(x => x.Date <= holidayRequestDto.ToDate);
        }

        return query.ToList();
    }

}
