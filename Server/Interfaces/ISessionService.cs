namespace SiGaHRMS.ApiService.Interfaces;

/// <summary>
/// Interface for managing session-related operations.
/// </summary>
public interface ISessionService
{
    /// <summary>
    /// Retrieves the ID of the current employee from the session.
    /// </summary>
    /// <returns>The ID of the current employee.</returns>
    long GetCurrentEmployeeId();
}

