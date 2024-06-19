namespace SiGaHRMS.Data.Entities.Api
{
    public class AssignRoleRequest
    {
        public AssignRoleRequest(
            string email,
            string roleName)
        {
            Email = email;
            RoleName = roleName;
        }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}
