using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsCompetition.Controllers;
using SportsCompetition.Enums;
using SportsCompetition.Persistance;

namespace SportsCompetition.Services
{
    public class RolesService
    {
        private readonly ILogger<RecordController> _logger;
        private readonly SportCompetitionDbContext _context;
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly IMapper _mapper;

        public RolesService(
            ILogger<RecordController> logger,
            SportCompetitionDbContext context,
            UserManager<IdentityUser<Guid>> userManager,
            IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IList<string>> GetRoles(Guid emloyeeId)
        {
            var result = new List<string>();
            try
            {
                var emp = _context.Employees
                .Include(e => e.User)
                .FirstOrDefault(e => e.Id == emloyeeId);

                IdentityUser<Guid> user = await _userManager.FindByIdAsync(emp.UserId.ToString());
                if (user != null)
                {
                    // получем список ролей пользователя
                    var userRoles = await _userManager.GetRolesAsync(user);

                    return userRoles;
                }
                else new Exception("User not exist");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
            return result;
        }

        public async Task<IEnumerable<IdentityUser<Guid>>> GetUser(Role role)
        {
            var users = await _userManager.GetUsersInRoleAsync(role.ToString());
            return users;
        }

        public async Task<string> Edit(Guid userId, Role role)
        {
            IdentityUser<Guid> user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                // получем список ролей пользователя
                var userRole = await _userManager.GetRolesAsync(user);

                await _userManager.AddToRoleAsync(user, role.ToString());

                await _userManager.RemoveFromRolesAsync(user, userRole);

                return "Ok";
            }

            return "User not exist";
        }
    }
}
