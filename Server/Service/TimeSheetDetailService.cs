using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.ApiService.Service;

public class TimeSheetDetailService : ITimeSheetDetailService
{
    private readonly ITimeSheetDetailRepository _timeSheetDetailRepository;
    private ILogger<TimeSheetDetailService> _logger;

    /// <summary>
    /// Initializes a new instance 
    /// </summary>
    /// <param name="ITimeSheetDetailRepository">dfhgdj</param>
    /// <param name="ILogger<TimeSheetDetailService>">gfhk</param>
    public TimeSheetDetailService(ITimeSheetDetailRepository timeSheetDetailRepository, ILogger<TimeSheetDetailService> logger)
    {
        _timeSheetDetailRepository = timeSheetDetailRepository;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task AddTimeSheetDetailAsync(TimeSheetDetail timeSheetDetail)
    {

        await _timeSheetDetailRepository.AddAsync(timeSheetDetail);
        await _timeSheetDetailRepository.CompleteAsync();
        _logger.LogInformation($"[AddTimeSheetDetailAsyns] - {timeSheetDetail.TimeSheetDetailId} added successfully");
    }

    /// <inheritdoc/>
    public async Task UpdateTimeSheetDetailAsync(TimeSheetDetail timeSheetDetail)
    {
        await _timeSheetDetailRepository.UpdateAsync(timeSheetDetail);
        await _timeSheetDetailRepository.CompleteAsync();
        _logger.LogInformation($"[UpdateTimeSheetDetailAsyns] - TimeSheetDetail updated successfully for the {timeSheetDetail.TimeSheetDetailId}");
    }

    /// <inheritdoc/>
    public async Task<TimeSheetDetail?> GetTimeSheetDetailByIdAsync(int id)
    {
        return await _timeSheetDetailRepository.
            FirstOrDefaultAsync(x => x.TimeSheetDetailId == id);
    }

    /// <inheritdoc/>
    public List<TimeSheetDetail> GetAllTimeSheetDetails()
    {
        var timeSheetDetailList = _timeSheetDetailRepository.GetAll();
        return (List<TimeSheetDetail>)timeSheetDetailList;
    }

    /// <inheritdoc/>
    public async Task DeleteTimeSheetDetailAsync(int timeSheetDetailId)
    {
        await _timeSheetDetailRepository.DeleteAsync(x => x.TimeSheetDetailId == timeSheetDetailId);
        await _timeSheetDetailRepository.CompleteAsync();
        _logger.LogInformation($"[DeleteTimeSheetDetailAsync] - TimeSheetDetail deleted successfully for the {timeSheetDetailId}");
    }

}
