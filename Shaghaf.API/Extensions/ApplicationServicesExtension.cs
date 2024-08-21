using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Shaghaf.Core.Entities.Identity;
using Shaghaf.Core.Services.Contract;
using Shaghaf.Infrastructure.Identity;
using Shaghaf.Service.AuthService;
using System.Text;

namespace Shaghaf.API.Extensions
{
    public static class ApplicationServicesExtension
    {

        public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            //    services.AddIdentity<ApplicationUser, IdentityRole>(options => { })
            //    .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })

    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = configuration["JWT:ValidIssuer"],
            ValidateAudience = true,
            ValidAudience = configuration["JWT:ValidAudience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:AuthKey"] ?? string.Empty)), // lw rg3 null 5leh string fadi 
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
        };

    });
            services.AddScoped(typeof(IAuthService), typeof(AuthService));

            return services;
        }
    }
}
