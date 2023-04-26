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
        private readonly ICacheService _cacheService;
        private readonly EmployeeService _employeeService;

        public EmployeeController(ILogger<SportsmanController> logger, SportCompetitionDbContext context, IMapper mapper, ICacheService cacheService, EmployeeService employeeService)
        {
            _cacheService = cacheService;
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _employeeService = employeeService;
        }

        [HttpGet("readAllEmployees")]
        public async Task<IEnumerable<GetEmployeeDto>> Get()
        {
            return _mapper.ProjectTo<GetEmployeeDto>(await _employeeService.GetAllEmployees());
        }

        [HttpPost("addEmployee")]
        public async Task<ActionResult> AddEmployee(AddEmployeeDto dto)
        {
            var employee = _mapper.Map<Employee>(dto);
            await _employeeService.AddEmployee(employee);
            return Ok();
        }

        [HttpPut("updateEmployee")]
        public async Task<ActionResult> UpdateEmployee(UpdateEmployeeDto dto)
        {
            var employee = _mapper.Map<Employee>(dto);
            await _employeeService.UpdateEmployee(employee);
            return Ok();
        }

        [HttpDelete("deleteEmployee/{id:Guid}")]
        public async Task<ActionResult> DeleteEmployee(Guid id)
        {
            await _employeeService.DeleteEmployee(id);
            return Ok();
        }
    }
}
