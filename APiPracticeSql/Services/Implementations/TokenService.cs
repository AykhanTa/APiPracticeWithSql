using ApiPractice.DAL.Entities;
using APiPracticeSql.Services.Interfaces;
using APiPracticeSql.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APiPracticeSql.Services.Implementations
{
    public class TokenService : ITokenService
    {
        public string GetToken(IList<string> userRoles,AppUser user, JwtSetting jwtSetting)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim("FullName",user.FullName),

            };
            claims.AddRange(userRoles.Select(r => new Claim("role", r)).ToList());

            var audience = jwtSetting.Audience;
            var issuer =jwtSetting.Issuer;

            var sectoken = new JwtSecurityToken(
                issuer,
                 audience,
                 claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(sectoken);
            return token;
        }
    }
}
