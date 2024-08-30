using ApiPractice.DAL.Entities;
using APiPracticeSql.Settings;

namespace APiPracticeSql.Services.Interfaces
{
    public interface ITokenService
    {
        string GetToken(IList<string> userRoles, AppUser user, JwtSetting jwtSetting);
    }
}
