using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsCompetition.Dtos;
using SportsCompetition.Models;
using SportsCompetition.Persistance;
using Microsoft.AspNetCore.Authorization;
using System;
using WebApplication1.Cache;

namespace SportsCompetition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<SportsmanController> _logger;
        private readonly ComposeApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public EmployeeController(ILogger<SportsmanController> logger, ComposeApiDbContext context, IMapper mapper, ICacheService cacheService)
        {
            _cacheService = cacheService;
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("readAllEmployees")]
        public async Task<IEnumerable<GetEmployeeDto>> Get()
        {
            const string key = "all-employees";
            var cached = _cacheService.GetValue<IEnumerable<GetEmployeeDto>>(key);

            if (cached == null)
            {
                var actual = _mapper.ProjectTo<GetEmployeeDto>(_context.Employees);
                if (actual.ToList().Count != 0)
                {
                    _cacheService.SetValue(key, actual);
                }

                return actual;
            }
            return cached;
        }

        [HttpPost("addSportsman")]
        public async Task<ActionResult> AddEmployees(AddEmployeeDto dto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var employee = _mapper.Map<Employee>(dto);

                await _context.AddAsync(employee);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500);
            }
        }

        [HttpPut("updateEmployee")]
        public async Task<ActionResult> UpdateEmployee(UpdateEmployeeDto dto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var employee = _mapper.Map<Employee>(dto);

                await _context.AddAsync(employee);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500);
            }
        }

        [HttpDelete("deleteEmployee/{id:Guid}")]
        public async Task<ActionResult> DeleteEmployee(Guid id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);

                if (employee == null)
                {
                    return NotFound();
                }

                _context.Remove(employee);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500);
            }
        }
    }
}
