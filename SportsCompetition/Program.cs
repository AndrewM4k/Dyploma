using ApiWithEF.Common;
using Microsoft.EntityFrameworkCore;
using SportsCompetition.Helpers;
using SportsCompetition.Persistance;
using System.Net.NetworkInformation;
using Microsoft.Extensions.DependencyInjection.Extensions;
using AutoMapper;
using Microsoft.OpenApi.Models;
using AutorisationApi.Services;
using AutorisationApi.Extensions;
using SportsCompetition.Services;
using WebApplication1.Cache;
using StackExchange.Redis;

namespace SportsCompetition
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddScoped<StreamService>();
            builder.Services.AddScoped<SecretaryService>();
            builder.Services.AddScoped<EventService>();
            builder.Services.AddScoped<SportsmanCompetitionService>();
            builder.Services.AddScoped<TokenService>();
            builder.Services.AddScoped<ICacheService, RedisCacheService>();

            builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            builder.Services.AddDbContext<ComposeApiDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddSingleton<IConnectionMultiplexer>(
                ConnectionMultiplexer.Connect(
                    builder.Configuration.GetConnectionString("Redis")));

            builder.Services.AddMemoryCache();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();
            app.Services.SeedDataContext();

            await app.Services.ApplyMigarationForDbContext<ComposeApiDbContext>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}