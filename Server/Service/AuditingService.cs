using SiGaHRMS.ApiService.Interfaces;
using System;
using System.Reflection;

public class AuditingService : IAuditingService
{
    private readonly ISessionService _sessionService;

    public AuditingService(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    public T SetAuditedEntity<T>(T entity) where T : class
    {
        var entityType = typeof(T);
        var currentDateTime = DateTime.UtcNow;
        var currentUserId = _sessionService.GetCurrentEmployeeId();

        SetPropertyIfExists(entity, entityType, "CreatedDateTime", currentDateTime, true);
        SetPropertyIfExists(entity, entityType, "CreatedBy", currentUserId, true);
        SetPropertyIfExists(entity, entityType, "DeletedDateTime", currentDateTime, false, () => IsDeleted(entity, entityType));
        SetPropertyIfExists(entity, entityType, "LastModifiedDateTime", currentDateTime, false);
        SetPropertyIfExists(entity, entityType, "LastModifiedBy", currentUserId, false);

        return entity;
    }

    private void SetPropertyIfExists<T>(T entity, Type entityType, string propertyName, object value, bool onlyIfNull, Func<bool> condition = null)
    {
        var property = entityType.GetProperty(propertyName);
        if (property != null && property.CanWrite)
        {
            var currentValue = property.GetValue(entity);
            if ((onlyIfNull && currentValue == null) || !onlyIfNull)
            {
                if (condition == null || condition())
                {
                    property.SetValue(entity, value);
                }
            }
        }
    }

    private bool IsDeleted<T>(T entity, Type entityType)
    {
        var property = entityType.GetProperty("IsDeleted");
        return property != null && (bool)property.GetValue(entity);
    }
}
