namespace SiGaHRMS.ApiService.Interfaces;

public interface IAuditingService
{
    T SetAuditedEntity<T>(T entity, bool created = false) where T : class;
}
