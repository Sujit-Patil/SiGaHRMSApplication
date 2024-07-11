namespace SiGaHRMS.ApiService.Interfaces;

public interface IDateTimeProvider
{
    public DateTime EstNow {  get; }
    public DateTime UtcNow {  get; }
    public DateTime Now { get; }
    public DateOnly Today {  get; }
    public DateTime? CastFromEstToUtc(DateTime? dateTime);
    public DateTime? CastFromUtcToEst(DateTime? dateTime);
    public DateOnly? CastToDateOnly(DateOnly? date);
}
