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
    [Authorize]
    public class RolesController : Controller
    {
        private readonly ILogger<RecordController> _logger;
        private readonly SportCompetitionDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public RolesController(UserManager<User> userManager, ILogger<RecordController> logger, SportCompetitionDbContext context, IMapper mapper)
        {
            _userManager = userManager;
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }
        [HttpGet("GetRoles")]
        public async Task<IActionResult> Get(Guid emloyeeId)
        {
            try
            {
                var emp = _context.Employees
                .Include(e => e.User)
                .FirstOrDefault(e => e.Id == emloyeeId);

                User user = await _userManager.FindByIdAsync(emp.UserId.ToString());
                if (user != null)
                {
                    // получем список ролей пользователя
                    var userRoles = await _userManager.GetRolesAsync(user);

                    return Ok(userRoles.ToList());
                }
            }
            catch (Exception)
            {
                throw;
            }
            return BadRequest( "User not exist");
        }

            [HttpGet("GetUsers")]
        public async Task<IEnumerable<User>> GetUser(Role role)
        {
            var users = await _userManager.GetUsersInRoleAsync(role.ToString());
            return users;
        }

            [HttpPost("EditById")]
        public async Task<IActionResult> Edit(Guid userId, Role role)
        {
            User user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                // получем список ролей пользователя
                var userRole = await _userManager.GetRolesAsync(user);
                
                await _userManager.AddToRoleAsync(user, role.ToString());

                await _userManager.RemoveFromRolesAsync(user, userRole);

                return Ok();
            }

            return BadRequest("User not exist");
        }
    }
}

