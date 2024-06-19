﻿namespace SiGaHRMS.Data.Entities.Api
{
    public class LoginRequest
    {
        public LoginRequest(
            string email,
            string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
