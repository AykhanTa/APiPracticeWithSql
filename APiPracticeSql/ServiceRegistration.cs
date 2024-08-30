using ApiPractice.DAL.Data;
using ApiPractice.DAL.Entities;
using APiPracticeSql.Dtos.GroupDtos;
using APiPracticeSql.Profiles;
using APiPracticeSql.Services.Implementations;
using APiPracticeSql.Services.Interfaces;
using APiPracticeSql.Settings;
using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
                opt.SignIn.RequireConfirmedEmail=true;
            }).AddEntityFrameworkStores<ApiPracticeContext>().AddDefaultTokenProviders();


            services.AddAuthentication(cfg => {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:SecretKey"]))
                };
            });

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmailService, EmailService>();
            services.Configure<JwtSetting>(config.GetSection("Jwt"));

        }
    }
}
