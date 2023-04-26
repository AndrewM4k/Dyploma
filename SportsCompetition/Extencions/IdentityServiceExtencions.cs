using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SportsCompetition.Models;
using SportsCompetition.Persistance;
using System.Text;

namespace SportsCompetition.Extencions
{
    public static class IdentityServiceExtencions
    {
        public static IServiceCollection AddIdentityservicer(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddIdentityCore<User>(opt => 
            {
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequiredLength = 10;
            })
                .AddSignInManager<SignInManager<User>>()
                .AddEntityFrameworkStores<SportCompetitionDbContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenSecret"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

            return services;
        }
    }
}
