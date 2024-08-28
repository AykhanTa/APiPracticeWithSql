using ApiPractice.DAL.Data;
using ApiPractice.DAL.Entities;
using APiPracticeSql.Dtos.GroupDtos;
using APiPracticeSql.Profiles;
using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace APiPracticeSql
{
    public static class ServiceRegistration
    {
        public static void Register(this IServiceCollection services,IConfiguration config)
        {
            services.AddControllers();


            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<GroupCreateDtoValidator>();
            services.AddFluentValidationRulesToSwagger();

            services.AddHttpContextAccessor();

            services.AddDbContext<ApiPracticeContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            services.AddAutoMapper(opt =>
            {
                opt.AddProfile(new MapProfile(new HttpContextAccessor()));
            });

            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric=true;
                opt.Password.RequiredLength=6;
                opt.Password.RequireUppercase=true;
                opt.Password.RequireLowercase=true;
                opt.Password.RequireDigit=true;
            }).AddEntityFrameworkStores<ApiPracticeContext>().AddDefaultTokenProviders();
        }
    }
}
