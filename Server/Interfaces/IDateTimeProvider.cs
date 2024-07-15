namespace SiGaHRMS.ApiService.Interfaces;

public interface IDateTimeProvider
{
    public DateTime EstNow {  get; }
    public DateTime UtcNow {  get; }
    public DateTime Now { get; }
    public DateOnly Today {  get; }
    public short GetYearFromDateOnly(DateOnly date);
    public short CalculateDateDifferenceInDays(DateOnly date1, DateOnly date2);
    public DateTime? CastFromEstToUtc(DateTime? dateTime);
    public DateTime? CastFromUtcToEst(DateTime? dateTime);
    public DateOnly? CastToDateOnly(DateOnly? date);
}
