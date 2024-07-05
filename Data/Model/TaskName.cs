
using System.ComponentModel.DataAnnotations;

namespace SiGaHRMS.Data.Model;

public class TaskName
{
    [Key]
    public int TaskId { get; set; }

    public string TaskDetails { get; set; }

}
