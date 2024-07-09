namespace SiGaHRMS.ApiService.Interfaces;

public interface IAuditingService
{
    T SetAuditedEntity<T>(T entity) where T : class;
}
