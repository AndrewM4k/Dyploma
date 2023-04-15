using Microsoft.EntityFrameworkCore;
using SportsCompetition.Models;
using SportsCompetition.Persistance;
using StackExchange.Redis;
using System.Linq;

namespace SportsCompetition
{
    public static class Seed
    {
        public static void SeedDataContext(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<ComposeApiDbContext>();

                _context.Database.Migrate();

                if (!_context.Role.Any())
                {
                    var roles = new List<Models.Role>()
                    {
                        new Models.Role("Adminisrator") {Description = "Adminisrator" },
                        new Models.Role("Judge") {Description = "Judge" },
                        new Models.Role("Secretary") {Description = "Secretary" },
                        new Models.Role("Assistant") {Description = "Assistant" }
                    };
                    
                    _context.Role.AddRange(roles);
                    _context.SaveChanges();
                };
                if (!_context.Employees.Any())
                {
                    var employees = new List<Employee>()
                    {
                        new Employee()
                        {
                            Name = "Judge",
                            Surname = "Judge",
                            Username = "Judge",
                            Password = "Judge",
                            Role = _context.Role.FirstOrDefault(a => a.Name == "Judge")
                        },
                        new Employee()
                        {
                            Name = "Secretary",
                            Surname = "Secretary",
                            Username = "Secretary",
                            Password = "Secretary",
                            Role = _context.Role.FirstOrDefault(a => a.Name == "Secretary")
                        },
                        new Employee()
                        {
                            Name = "Assistant",
                            Surname = "Assistant",
                            Username = "Assistant",
                            Password = "Assistant",
                            Role = _context.Role.FirstOrDefault(a => a.Name == "Assistant")
                        }
                    };

                    _context.Employees.AddRange(employees);
                    _context.SaveChanges();
                };
                if (!_context.Competition.Any())
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

                    _context.Competition.AddRange(competitions);
                    _context.SaveChanges();
                };
            }
        }
    }
}
