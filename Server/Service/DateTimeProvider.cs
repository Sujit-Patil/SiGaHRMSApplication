using SiGaHRMS.ApiService.Interfaces;
using System.Runtime.InteropServices;

namespace SiGaHRMS.ApiService.Service;

// <summary>
/// Provider which allow for getting the current any date/time.
/// </summary>
public class DateTimeProvider : IDateTimeProvider
{
    /// <summary>
    /// Gets the current EST date/time.
    /// </summary>
    public DateTime EstNow => RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
              TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")) :
              TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("America/New_York"));

    /// <summary>
    /// Gets the current UTC date/time.
    /// </summary>
    public DateTime UtcNow => DateTime.UtcNow;

    /// <summary>
    /// Gets the current date/time.
    /// </summary>
    public DateTime Now => DateTime.Now;

    /// <summary>
    /// Gets the current date/time.
    /// </summary>
    public DateOnly Today => DateOnly.FromDateTime(DateTime.Today);

    /// <summary>
    /// Used to convert from Est format to Utc.
    /// </summary>
    /// <param name="dateTime">dateTime to convert.</param>
    /// <exception cref="ArgumentNullException">Thrown when any of the following properties are set null: data-time.</exception>
    /// <returns>The date-time requested based on the format.</returns>
    public DateTime? CastFromEstToUtc(DateTime? dateTime)
    {
        if (dateTime is null)
        {
            return null;
        }
        DateTime? result = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
             TimeZoneInfo.ConvertTimeToUtc(dateTime.Value, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")) :
             TimeZoneInfo.ConvertTimeToUtc(dateTime.Value, TimeZoneInfo.FindSystemTimeZoneById("America/New_York"));
        return result;
    }

    /// <summary>
    /// Used to convert from Utc format to Est.
    /// </summary>
    /// <param name="dateTime">dateTime to convert.</param>
    /// <exception cref="ArgumentNullException">Thrown when any of the following properties are set null: data-time.</exception>
    /// <returns>The date-time requested based on the format.</returns>
    public DateTime? CastFromUtcToEst(DateTime? dateTime)
    {
        if (dateTime is null)
        {
            return null;
        }
        DateTime? result = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
              TimeZoneInfo.ConvertTime(dateTime.Value, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")) :
              TimeZoneInfo.ConvertTime(dateTime.Value, TimeZoneInfo.FindSystemTimeZoneById("America/New_York"));
        return result;
    }

    public DateOnly? CastToDateOnly(DateOnly? date) => (DateOnly?)date;
}
