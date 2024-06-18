namespace SiGaHRMS.Data.Entities.Api
{
    public class RegistrationRequest
    {
        public RegistrationRequest(
            string email,
            string name,
            string phoneNumber,
            string password)
        {
            Email = email;
            Name = name;
            PhoneNumber = phoneNumber;
            Password = password;
        }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }
    }
}
