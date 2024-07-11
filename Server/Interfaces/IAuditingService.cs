namespace SiGaHRMS.ApiService.Interfaces;

public interface IAuditingService
{
    T SetAuditedEntity<T>(T entity, bool created) where T : class;
}
