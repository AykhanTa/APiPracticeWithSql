using Microsoft.AspNetCore.Identity;

namespace ApiPractice.DAL.Entities
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
