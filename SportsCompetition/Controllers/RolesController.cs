using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SportsCompetition.Dtos;
using SportsCompetition.Enums;
using SportsCompetition.Filters;
using SportsCompetition.Models;
using SportsCompetition.Persistance;
using SportsCompetition.Services;

namespace SportsCompetition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize(Role.Administrator)]
    public class RolesController : Controller
    {
        private readonly ILogger<RecordController> _logger;
        private readonly SportCompetitionDbContext _context;
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly IMapper _mapper;
        private readonly RolesService _rolesService;

        public RolesController(
            UserManager<IdentityUser<Guid>> userManager,
            ILogger<RecordController> logger,
            SportCompetitionDbContext context,
            IMapper mapper,
            RolesService rolesService)
        {
            _userManager = userManager;
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _rolesService = rolesService;
        }
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid emloyeeId)
        {
            var result = await _rolesService.GetRoles(emloyeeId);
            if (result.Any())
            {
                return Ok(result);
            }
            else return BadRequest();
        }

        [HttpGet("getUsersByRole")]
        public async Task<IEnumerable<IdentityUser<Guid>>> GetUser(Role role)
        {
            return await _rolesService.GetUser(role);
        }

        [HttpPut("editRoleById")]
        public async Task<IActionResult> Edit(Guid userId, Role role)
        {
            var result = await _rolesService.Edit(userId, role);
            if (result == "Ok")
            {
                return Ok();
            }

            return BadRequest(result);
        }
    }
}

