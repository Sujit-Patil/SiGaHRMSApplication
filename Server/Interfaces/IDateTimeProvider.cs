namespace SiGaHRMS.ApiService.Interfaces;

/// <summary>
/// Interface for providing date and time operations.
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// Gets the current local time in Eastern Standard Time (EST).
    /// </summary>
    DateTime EstNow { get; }

    /// <summary>
    /// Gets the current Coordinated Universal Time (UTC).
    /// </summary>
    DateTime UtcNow { get; }

    /// <summary>
    /// Gets the current local time.
    /// </summary>
    DateTime Now { get; }

    /// <summary>
    /// Gets the current date.
    /// </summary>
    DateOnly Today { get; }

    /// <summary>
    /// Converts a DateOnly instance to DateTime.
    /// </summary>
    /// <param name="date">The DateOnly instance to convert.</param>
    /// <returns>The DateTime representation of the DateOnly instance.</returns>
    public DateTime CastDateOnlyToDateTime(DateOnly date);


    DateOnly CastDateTimeToDateOnly(DateTime datetime);

    /// <summary>
    /// Gets the last date of the month for the specified DateOnly instance.
    /// </summary>
    /// <param name="date">The DateOnly instance representing the month.</param>
    /// <returns>The DateOnly instance representing the last date of the month.</returns>
    DateOnly LastDateOfMonth(DateOnly date);

    /// <summary>
    /// Gets the year from the specified DateOnly instance.
    /// </summary>
    /// <param name="date">The DateOnly instance.</param>
    /// <returns>The year extracted from the DateOnly instance.</returns>
    short GetYearFromDateOnly(DateOnly date);

    /// <summary>
    /// Calculates the difference in days between two DateOnly instances.
    /// </summary>
    /// <param name="date1">The first DateOnly instance.</param>
    /// <param name="date2">The second DateOnly instance.</param>
    /// <returns>The difference in days between the two DateOnly instances.</returns>
    short CalculateWorkingDateDifferenceInDays(DateOnly startDate, DateOnly endDate);

    /// <summary>
    /// Converts a nullable DateTime from Eastern Standard Time (EST) to Coordinated Universal Time (UTC).
    /// </summary>
    /// <param name="dateTime">The nullable DateTime in EST.</param>
    /// <returns>The nullable DateTime converted to UTC.</returns>
    DateTime? CastFromEstToUtc(DateTime? dateTime);

    /// <summary>
    /// Converts a nullable DateTime from Coordinated Universal Time (UTC) to Eastern Standard Time (EST).
    /// </summary>
    /// <param name="dateTime">The nullable DateTime in UTC.</param>
    /// <returns>The nullable DateTime converted to EST.</returns>
    DateTime? CastFromUtcToEst(DateTime? dateTime);

    /// <summary>
    /// Converts a nullable DateOnly instance to a nullable DateTime instance.
    /// </summary>
    /// <param name="date">The nullable DateOnly instance to convert.</param>
    /// <returns>The nullable DateTime representation of the DateOnly instance.</returns>
    DateOnly? CastToDateOnly(DateOnly? date);
}

