using ApiPractice.DAL.Entities;
using APiPracticeSql.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ApiPractice.DAL.Data
{
    public class ApiPracticeContext : IdentityDbContext<AppUser>
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Author> Authors { get; set; }
        public ApiPracticeContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //seedData
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Name = "Super-Admin",
                    NormalizedName = "SUPER-USER"
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);

            var hasher = new PasswordHasher<AppUser>();
            List<AppUser> users = new List<AppUser>
            {
                new AppUser
                {
                    UserName = "adminuser",
                    Email = "admin@example.com",
                    NormalizedUserName = "ADMINUSER",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    PasswordHash = hasher.HashPassword(null, "AdminPassword123!"),
                    EmailConfirmed = true,
                },
            };
            modelBuilder.Entity<AppUser>().HasData(users);

            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = roles.First(r => r.Name == "Admin").Id,
                    UserId = users.First(u => u.UserName == "adminuser").Id
                },
            };
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);

            base.OnModelCreating(modelBuilder);

        }
    }
}
