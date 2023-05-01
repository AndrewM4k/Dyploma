using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsCompetition.Controllers;
using SportsCompetition.Dtos;
using SportsCompetition.Enums;
using SportsCompetition.Models;
using SportsCompetition.Persistance;
using System.Linq;
using WebApplication1.Cache;

namespace SportsCompetition.Services
{
    public class EmployeeService
    {
        private readonly ILogger<SportsmanController> _logger;
        private readonly SportCompetitionDbContext _context;
        private readonly ICacheService _cacheService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public EmployeeService(ILogger<SportsmanController> logger, SportCompetitionDbContext context, ICacheService cacheService, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _cacheService = cacheService;
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            const string key = "all-employees";
            var cached = _cacheService.GetValue<List<Employee>>(key);

            if (cached == null)
            {
                var actual = _context.Employees.ToList();
                if (actual.ToList().Count != 0)
                {
                    _cacheService.SetValue(key, actual);
                }
                return actual;
            }
            return cached;
        }

        public async Task AddEmployee(Employee employee, string username, string password, Role role)
        {
            const string key = "all-employees";
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                employee.User = new User()
                {
                    Id = new Guid(),
                    UserName = username,
                    NormalizedUserName = username.ToUpper()
                };
                employee.Role = role;

                await _userManager.CreateAsync(employee.User, $"{password}");
                await _userManager.AddToRoleAsync(employee.User, employee.Role.ToString());

                _context.Employees.Add(employee);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }

            await transaction.CommitAsync();

            _cacheService.UpdateValue(key);
        }

        public async Task UpdateEmployee(Employee employee)
        {
            const string key = "all-employees";
            using var transaction = _context.Database.BeginTransaction();

            await _context.AddAsync(employee);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            await transaction.RollbackAsync();

            _cacheService.UpdateValue(key);
        }

        public async Task DeleteEmployee(Guid id)
        {
            const string key = "all-employees";
            using var transaction = _context.Database.BeginTransaction();

            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return;
            }
            _context.Remove(employee);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            _cacheService.UpdateValue(key);
        }
    }
}
