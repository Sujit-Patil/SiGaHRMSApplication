using Microsoft.AspNetCore.Identity;

namespace SiGaHRMS.Data.Model.AuthModel;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
}
