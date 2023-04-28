using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

                //if (role.Name == "Adminisrator")
                //{
                //       var user = new User()
                //       {
                //            Email = "andrey.mar4uk2011@yandex.ru",
                //            EmailConfirmed = true,
                //            UserName = "Adminisrator",
                //            NormalizedUserName = "ANDREY.MAR4UK2011@YANDEX.RU"
                //       };


                //await userManager.CreateAsync(user,
                //    $"{{u2w]B&kz;{role.Name}");

                if (!context.Employees.Any())
                {
                    await userManager.CreateAsync(new User()
                    {
                        Email = "andrey.mar4uk2011@yandex.ru",
                        EmailConfirmed = true,
                        UserName = "Adminisrator",
                        NormalizedUserName = "ANDREY.MAR4UK2011@YANDEX.RU"
                    },
                            $"{{u2w]B&kz;Adminisrator");

                    await userManager.CreateAsync(new User()
                    {
                        UserName = "Judge",
                        NormalizedUserName = "JUDJE"
                    },
                            $"{{u2w]B&kz;Judge");

                    await userManager.CreateAsync(new User()
                    {
                        UserName = "Secretary",
                        NormalizedUserName = "SECRETARY"
                    },
                            $"{{u2w]B&kz;Secretary");

                    await userManager.CreateAsync(new User()
                    {
                        UserName = "Assistant",
                        NormalizedUserName = "ASSISTANT"
                    },
                            $"{{u2w]B&kz;Assistant");

                    var employees = new List<Employee>()
                    {
                        new Employee()
                        {
                            Name = "Judge",
                            Surname = "Judge",
                            Role = Enums.Role.Judge,
                            User = context.Users.FirstOrDefault(u=>u.UserName == "Judge")
                        },
                        new Employee()
                        {
                            Name = "Secretary",
                            Surname = "Secretary",
                            Role = Enums.Role.Secretary,
                            User = context.Users.FirstOrDefault(u=>u.UserName == "Secretary")
                        },
                        new Employee()
                        {
                            Name = "Assistant",
                            Surname = "Assistant",
                            Role = Enums.Role.Assistant,
                            User = context.Users.FirstOrDefault(u=>u.UserName == "Assistant")
                        },
                        new Employee()
                        {
                            Name = "Adminisrator",
                            Surname = "Adminisrator",
                            Role = Enums.Role.Administrator,
                            User = context.Users.FirstOrDefault(u=>u.UserName == "Adminisrator")
                        }
                    };
                    await userManager.AddToRoleAsync(context.Users.FirstOrDefault(u => u.UserName == "Judge"), "Judge");
                    await userManager.AddToRoleAsync(context.Users.FirstOrDefault(u => u.UserName == "Adminisrator"), "Adminisrator");
                    await userManager.AddToRoleAsync(context.Users.FirstOrDefault(u => u.UserName == "Assistant"), "Assistant");
                    await userManager.AddToRoleAsync(context.Users.FirstOrDefault(u => u.UserName == "Secretary"), "Secretary");

                    context.Employees.AddRange(employees);
                    context.SaveChanges();

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
