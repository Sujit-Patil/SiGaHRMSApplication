using SiGaHRMS.Data.Model.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiGaHRMS.Data.Model;

public class User : FullAuditedEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long UserId { get; set; }

    [Required]
    [MaxLength(50)]
    public string UserName { get; set; }

    [Required]
    public string PasswordPhrase { get; set; }

    [Required]
    public string PasswordSalt { get; set; }

    public bool IsActive { get; set; }

}


