using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsCompetition.Dtos;
using SportsCompetition.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using WebApplication1.Cache;
using SportsCompetition.Services;
using SportsCompetition.Persistance;

namespace SportsCompetition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<SportsmanController> _logger;
        private readonly SportCompetitionDbContext _context;
        private readonly IMapper _mapper;
        private readonly EmployeeService _employeeService;
        private readonly SportsmanCompetitionService _sportsmanCompetitionService;

        public EmployeeController(ILogger<SportsmanController> logger, SportCompetitionDbContext context, IMapper mapper, EmployeeService employeeService, SportsmanCompetitionService sportsmanCompetitionService = null)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _employeeService = employeeService;
            _sportsmanCompetitionService = sportsmanCompetitionService;
        }

        [HttpGet("readAllEmployees")]
        public async Task<IEnumerable<GetEmployeeDto>> Get()
        {
            var employees = new List<GetEmployeeDto>();
            try
            {
                foreach (var item in await _employeeService.GetAllEmployees())
                {
                    employees.Add(_mapper.Map<GetEmployeeDto>(item));
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
            return employees;
        }

        [HttpPost("addEmployee")]
        public async Task<IActionResult> AddEmployee(AddEmployeeDto dto)
        {
            var username = dto.Username;
            var password = dto.Password;
            var role = dto.Role;
            var employee = _mapper.Map<Employee>(dto);
            await _employeeService.AddEmployee(employee, username, password, role);
            return Ok();
        }

        [HttpPut("updateEmployee")]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDto dto)
        {
            var employee = _mapper.Map<Employee>(dto);
            await _employeeService.UpdateEmployee(employee);
            return Ok();
        }

        [HttpDelete("deleteEmployee/{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            await _employeeService.DeleteEmployee(id);
            return Ok();
        }

        [HttpGet("getSportsmanCompetitionAttemptWeight")]
        public async Task<int> GetAtteptWeight(Guid sportsmanCompetition, int atempt)
        {
            return await _sportsmanCompetitionService.GetAtteptWeight(sportsmanCompetition, atempt);
        }
        [HttpGet("setSportsmanCompetitionAttemptWeight")]
        public async Task<SportsmanCompetition> SetWeight(Guid sportsmanCompetition, int attemptNumber, int weight)
        {
            return await _sportsmanCompetitionService.SetWeight(sportsmanCompetition, attemptNumber, weight);
        }
    }
}
