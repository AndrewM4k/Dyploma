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
using FluentValidation;
using SportsCompetition.Filters;
using SportsCompetition.Enums;

namespace SportsCompetition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize(Role.Administrator, Role.Secretary)]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<SportsmanController> _logger;
        private readonly SportCompetitionDbContext _context;
        private readonly IMapper _mapper;
        private readonly EmployeeService _employeeService;
        private readonly SportsmanCompetitionService _sportsmanCompetitionService;
        private readonly IValidator<AddEmployeeDto> _validator;

        public EmployeeController(
            ILogger<SportsmanController> logger, 
            SportCompetitionDbContext context, 
            IMapper mapper, EmployeeService employeeService, 
            SportsmanCompetitionService sportsmanCompetitionService, 
            IValidator<AddEmployeeDto> validator)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _employeeService = employeeService;
            _sportsmanCompetitionService = sportsmanCompetitionService;
            _validator = validator;
        }

        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeDto dto)
        {
            var validateresult = await _validator.ValidateAsync(dto);
            if (!validateresult.IsValid)
            {
                return BadRequest(validateresult.Errors.ToList());
            }
            var username = dto.Username;
            var password = dto.Password;
            var role = dto.Role;
            var employee = _mapper.Map<Employee>(dto);
            await _employeeService.AddEmployee(employee, username, password, role);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDto dto)
        {
            var employee = _mapper.Map<Employee>(dto);
            await _employeeService.UpdateEmployee(employee);
            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            await _employeeService.DeleteEmployee(id);
            return Ok();
        }

        [HttpGet("getSportsmanCompetitionAttemptWeight")]
        public async Task<int> GetAtteptWeight(Guid sportsmanCompetition, int attempt)
        {
            return await _sportsmanCompetitionService.GetAtteptWeight(sportsmanCompetition, attempt);
        }

        [HttpGet("setSportsmanCompetitionAttemptWeight")]
        public async Task<int> SetWeight(Guid sportsmanCompetition, int attemptNumber, int weight)
        {
            return await _sportsmanCompetitionService.SetWeight(sportsmanCompetition, attemptNumber, weight);
        }
    }
}
