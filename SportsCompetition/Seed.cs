using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportsCompetition.Models;
using SportsCompetition.Persistance;

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
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser<Guid>>>();

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
                }
                if (!context.Employees.Any())
                {
                    var employees = new List<Employee>()
                    {
                        new Employee()
                        {
                            Name = "Judge",
                            Surname = "Judge",
                            Role = Enums.Role.Judge,
                            User = new IdentityUser<Guid>()
                            {
                                UserName = "Judge",
                                NormalizedUserName = "JUDJE"
                            }
                        },
                        new Employee()
                        {
                            Name = "Secretary",
                            Surname = "Secretary",
                            Role = Enums.Role.Secretary,
                            User = new IdentityUser<Guid>()
                            {
                                UserName = "Secretary",
                                NormalizedUserName = "SECRETARY"
                            }
                        },
                        new Employee()
                        {
                            Name = "Assistant",
                            Surname = "Assistant",
                            Role = Enums.Role.Assistant,
                            User = new IdentityUser<Guid>()
                            {
                                UserName = "Assistant",
                                NormalizedUserName = "ASSISTANT"
                            }
                        },
                        new Employee()
                        {
                            Name = "Adminisrator",
                            Surname = "Adminisrator",
                            Role = Enums.Role.Administrator,
                            User = new IdentityUser<Guid>()
                            {
                                Email = "andrey.mar4uk2011@yandex.ru",
                                EmailConfirmed = true,
                                UserName = "Adminisrator",
                                NormalizedUserName = "ANDREY.MAR4UK2011@YANDEX.RU"
                            }
                        }
                    };

                    foreach (var employee in employees)
                    {
                        await userManager.CreateAsync(employee.User, $"Password_04${employee.Name}");
                        await userManager.AddToRoleAsync(employee.User, employee.Role.ToString());
                    }

                    context.Employees.AddRange(employees);
                    context.SaveChanges();
                }

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
                }
            }
        }
    }
}
