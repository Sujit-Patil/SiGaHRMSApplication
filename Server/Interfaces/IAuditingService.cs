namespace SiGaHRMS.ApiService.Interfaces;

/// <summary>
/// Interface for auditing services that provide methods to audit entities.
/// </summary>
public interface IAuditingService
{
    /// <summary>
    /// Sets auditing information for the specified entity.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="entity">The entity to be audited.</param>
    /// <param name="created">True if the entity is being created; false if it is being updated.</param>
    /// <returns>The audited entity.</returns>
    T SetAuditedEntity<T>(T entity, bool created) where T : class;
}
