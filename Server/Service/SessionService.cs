using SiGaHRMS.ApiService.Interfaces;

namespace SiGaHRMS.ApiService.Service;

/// <summary>
/// Session service implementation.
/// </summary>
public class SessionService : ISessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    /// <summary>
    /// Initializes a new instance of the <see cref="SessionService"/> class.
    /// </summary>
    /// <param name="httpContextAccessor">HttpContextAccessor interface.</param>
    public SessionService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <inheritdoc/>
    public long GetCurrentEmployeeId()
    {
        return long.Parse(_httpContextAccessor.HttpContext.User.FindFirst("employeeId").Value);
    }
}
