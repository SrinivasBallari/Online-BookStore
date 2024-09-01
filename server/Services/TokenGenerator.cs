using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using server.Models.DB;

namespace server.Services
{
    public class JwtTokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            string audience, issuer, secret;
            audience = _configuration.GetValue<string>("Audience")!;
            issuer = _configuration.GetValue<string>("Issuer")!;
            secret = _configuration.GetValue<string>("Secret")!;

            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Iss,issuer),
                new Claim(JwtRegisteredClaimNames.Aud,audience),
                new Claim(ClaimTypes.Role,user.Role),
                new Claim(ClaimTypes.Email,user.Email)
            };

            byte[] secretBytes=System.Text.Encoding.UTF8.GetBytes(secret);
            var key=new SymmetricSecurityKey(secretBytes);

            var signingCredentials=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.Now.AddMinutes(60), 
                 signingCredentials:signingCredentials);

            var handler = new JwtSecurityTokenHandler();

            string token=handler.WriteToken(securityToken);

            return token;
        }
    }
}
