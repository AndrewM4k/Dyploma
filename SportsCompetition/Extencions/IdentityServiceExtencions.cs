﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SportsCompetition.Models;
using SportsCompetition.Persistance;
using System.Text;

namespace SportsCompetition.Extencions
{
    public static class IdentityServiceExtencions
    {
        public static IServiceCollection AddIdentityServicer(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddIdentityCore<IdentityUser<Guid>>(opt => 
            {
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequiredLength = 10;
            })
                .AddRoles<IdentityRole<Guid>>()
                .AddRoleManager<RoleManager<IdentityRole<Guid>>>()
                .AddUserManager<UserManager<IdentityUser<Guid>>>()
                .AddSignInManager<SignInManager<IdentityUser<Guid>>>()
                .AddRoleValidator<RoleValidator<IdentityRole<Guid>>>()
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
