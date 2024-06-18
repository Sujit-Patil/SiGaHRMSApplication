
namespace SiGaHRMS.Data.Model.Entity;

public abstract class FullAuditedEntity
{
    public bool IsDeleted { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    public long? LastModifiedBy { get; set; }
    public DateTime? LastModifiedDateTime { get; set; }
    public long? DeletedBy { get; set; }
    public DateTime? DeletedDateTime { get; set; }
}
