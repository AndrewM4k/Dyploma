using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsCompetition.Controllers;
using SportsCompetition.Dtos;
using SportsCompetition.Models;
using SportsCompetition.Persistance;
using WebApplication1.Cache;

namespace SportsCompetition.Services
{
    public class EmployeeService
    {
        private readonly ILogger<SportsmanController> _logger;
        private readonly SportCompetitionDbContext _context;
        private readonly ICacheService _cacheService;

        public EmployeeService(ILogger<SportsmanController> logger, SportCompetitionDbContext context, ICacheService cacheService)
        {
            _cacheService = cacheService;
            _logger = logger;
            _context = context;
        }

        public async Task<IQueryable<Employee>> GetAllEmployees()
        {
            const string key = "all-employees";
            var cached = _cacheService.GetValue<IQueryable<Employee>>(key);

            if (cached == null)
            {
                var actual = _context.Employees;
                if (actual.ToList().Count != 0)
                {
                    _cacheService.SetValue(key, actual);
                }
                return actual;
            }
            return cached;
        }

        public async Task AddEmployee(Employee employee)
        {
            const string key = "all-employees";
            using var transaction = _context.Database.BeginTransaction();

            await _context.AddAsync(employee);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            await transaction.RollbackAsync();

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
