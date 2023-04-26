using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportsCompetition.Enums;
using SportsCompetition.Models;
using SportsCompetition.Persistance;
using StackExchange.Redis;
using System.Linq;
using System.Xml.Linq;

namespace SportsCompetition
{
    public static class Seed
    {
        public static async Task SeedDataContext(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SportCompetitionDbContext>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
                var userManager = scope.ServiceProvider.GetService<UserManager<User>>();

                context.Database.Migrate();

                if (!roleManager.Roles.Any())
                {
                    var roles = new List<IdentityRole<Guid>>()
                    {
                        new() { Name = Enums.Role.Administrator.ToString() },
                        new() { Name = Enums.Role.Judge.ToString() },
                        new() { Name = Enums.Role.Secretary.ToString() },
                        new() { Name = Enums.Role.Assistant.ToString() },
                        new() { Name = Enums.Role.Sportsman.ToString() },
                    };
                    foreach (var item in roles)
                    {
                        await roleManager.CreateAsync(item);
                    }
                };

                if (!context.Employees.Any())
                {
                    var employees = new List<Employee>()
                    {
                        new Employee()
                        {
                            Name = "Judge",
                            Surname = "Judge",
                            User = new User()
                            {
                                UserName = "Judge_01",
                                NormalizedUserName = "JUDGE_01"
                            },
                            Role = Enums.Role.Judge

                        },
                        new Employee()
                        {
                            Name = "Secretary",
                            Surname = "Secretary",
                            User = new User()
                            {
                                UserName = "Secretary_01",
                                NormalizedUserName = "SECRETARY_01"
                            },
                            Role = Enums.Role.Secretary
                        },
                        new Employee()
                        {
                            Name = "Assistant",
                            Surname = "Assistant",
                            User = new User()
                            {
                                UserName = "Assistant_01",
                                NormalizedUserName = "ASSISTANT_01"
                            },
                            Role = Enums.Role.Assistant
                        },
                        new Employee()
                        {
                            Name = "Adminisrator",
                            Surname = "Adminisrator",
                            User = new User()
                            {
                                Email = "andrey.mar4uk2011@yandex.ru",
                                EmailConfirmed = true,
                                UserName = "andrey.mar4uk2011@yandex.ru",
                                NormalizedUserName = "ANDREY.MAR4UK2011@YANDEX.RU"
                            },
                            Role = Enums.Role.Administrator
                        }

                    };
                    context.Employees.AddRange(employees);
                    context.SaveChanges();

                    foreach (var employee in employees)
                    {
                        await userManager.CreateAsync(employee.User, "Password_04${employee.Name}");
                        await userManager.AddToRoleAsync(employee.User, employee.Role.ToString());
                    } 
                    
                };
                if (!context.Competition.Any())
                {
                    var competitions = new List<Competition>()
                    {
                        new Competition()
                        {
                            Name = "Bencpress",
                            Equpment = "Raw"
                        },
                        new Competition()
                        {
                            Name = "Squat",
                            Equpment = "Raw"
                        },
                        new Competition()
                        {
                            Name = "Deadlift",
                            Equpment = "Raw"
                        }
                    };

                    context.Competition.AddRange(competitions);
                    context.SaveChanges();
                };
            }
        }
    }
}
