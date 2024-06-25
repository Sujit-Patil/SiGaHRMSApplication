using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Service;

public class HolidayService : IHolidayService
{
    private readonly IHolidayRepository _holidayRepository;
    private ILogger<HolidayService> _logger;

    /// <summary>
    /// Initializes a new instance 
    /// </summary>
    /// <param name="IHolidayRepository">dfhgdj</param>
    /// <param name="ILogger<HolidayService>">gfhk</param>
    public HolidayService(IHolidayRepository holidayRepository, ILogger<HolidayService> logger)
    {
        _holidayRepository = holidayRepository;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task AddHolidayAsync(Holiday holiday)
    {

        await _holidayRepository.AddAsync(holiday);
        await _holidayRepository.CompleteAsync();
        _logger.LogInformation($"[AddHolidayAsyns] - {holiday.HolidayId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateHolidayAsync(Holiday holiday)
    {
        await _holidayRepository.UpdateAsync(holiday);
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
    public List<Holiday> GetAllHolidays()
    {
        var holidayList = _holidayRepository.GetAll();
        return (List<Holiday>)holidayList;
    }

    /// <inheritdoc/>
    public async Task DeleteHolidayAsync(int holidayId)
    {
        await _holidayRepository.DeleteAsync(x => x.HolidayId == holidayId);
        await _holidayRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteHolidayAsync] - Holiday deleted successfully for the {holidayId}");
    }

}
