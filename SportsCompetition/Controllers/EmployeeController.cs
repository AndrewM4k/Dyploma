using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsCompetition.Dtos;
using SportsCompetition.Models;
using SportsCompetition.Persistance;
using Microsoft.AspNetCore.Authorization;

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

        public EmployeeController(ILogger<SportsmanController> logger, ComposeApiDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("readAllEmployees")]
        public async Task<IEnumerable<GetEmployeeDto>> Get()
        {
            return _mapper.ProjectTo<GetEmployeeDto>(_context.Employees);
        }

        [HttpPost("addSportsman")]
        public async Task<ActionResult> AddSportsman(AddEmployeeDto dto)
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
        public async Task<ActionResult> UpdateSportsman(UpdateEmployeeDto dto)
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
        public async Task<ActionResult> DeleteSportsman(Guid id)
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
