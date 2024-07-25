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
    /// Gets the today date.
    /// </summary>
    public DateOnly Today => DateOnly.FromDateTime(DateTime.Today);

    /// <summary>
    /// Cast the DateOnly date to dateTime.
    /// </summary>
    public DateTime CastDateOnlyToDateTime(DateOnly date)
    {
        return date.ToDateTime(TimeOnly.MinValue);
    }

    public DateOnly CastDateTimeToDateOnly(DateTime dateTime)
    {
        return new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
    }

    /// <summary>
    /// Gets the last date month.
    /// </summary>
    public DateOnly LastDateOfMonth(DateOnly date)
    {
        int year = date.Year;
        int month = date.Month;

        DateOnly firstDayOfNextMonth;
        if (month == 12)
        {
            firstDayOfNextMonth = new DateOnly(year + 1, 1, 1);
        }
        else
        {
            firstDayOfNextMonth = new DateOnly(year, month + 1, 1);
        }

        DateOnly lastDayOfMonth = firstDayOfNextMonth.AddDays(-1);

        return lastDayOfMonth;
    }

    /// <summary>
    /// Gets the year using date.
    /// </summary>
    public short GetYearFromDateOnly(DateOnly date) => (short)date.Year;

    /// <summary>
    /// Gets Date Difference InDays.
    /// </summary>
    public short CalculateWorkingDateDifferenceInDays(DateOnly startDate, DateOnly endDate)
    {
        if (startDate > endDate)
            throw new ArgumentException("Start date cannot be later than end date.");

        DateTime dateTimeStart = startDate.ToDateTime(new TimeOnly());
        DateTime dateTimeEnd = endDate.ToDateTime(new TimeOnly());

        int totalDays = (dateTimeEnd - dateTimeStart).Days + 1;
        int fullWeeks = totalDays / 7;
        int extraDays = totalDays % 7;

        int weekendDays = fullWeeks * 2;
        for (int i = 0; i < extraDays; i++)
        {
            if (dateTimeStart.AddDays(fullWeeks * 7 + i).DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
                weekendDays++;
        }

        return (short)(totalDays - weekendDays);
    }


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
