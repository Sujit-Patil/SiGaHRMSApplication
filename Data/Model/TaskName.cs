
using SiGaHRMS.Data.Model.Entity;
using System.ComponentModel.DataAnnotations;

namespace SiGaHRMS.Data.Model;

public class TaskName : FullAuditedEntity
{
    [Key]
    public int TaskId { get; set; }

    public string TaskDetails { get; set; }

    public int? ProjectId { get; set; }

    public Project? Project { get; set; }

    public int? ClientId { get; set; }

    public Client? Client { get; set; }

}
