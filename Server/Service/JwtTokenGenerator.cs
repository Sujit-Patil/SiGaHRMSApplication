using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.AuthModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SiGaHRMS.ApiService.Service
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;

        /// <summary>
        /// Initializes a new instance of the JwtTokenGenerator class.
        /// </summary>
        /// <param name="jwtOptions">The JWT options configured for token generation.</param>
        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }


        /// <inheritdoc/>
        public string GenerateToken(IdentityUser identityUser, IEnumerable<string> roles, Employee? employee)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var claimList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
                new Claim(JwtRegisteredClaimNames.Sub, identityUser.Id),
                new Claim(JwtRegisteredClaimNames.Name, identityUser.UserName),
                new Claim("employeeId",employee.EmployeeId.ToString())
            };

            claimList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
