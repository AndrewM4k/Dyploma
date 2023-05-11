using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsCompetition.Controllers;
using SportsCompetition.Dtos;
using SportsCompetition.Enums;
using SportsCompetition.Models;
using SportsCompetition.Persistance;
using WebApplication1.Cache;

namespace SportsCompetition.Services
{
    public class SportsmanService
    {
        private readonly ILogger<SportsmanController> _logger;
        private readonly SportCompetitionDbContext _context;
        private readonly ICacheService _cacheService;
        private readonly UserManager<IdentityUser<Guid>> _userManager;

        public SportsmanService(ILogger<SportsmanController> logger, SportCompetitionDbContext context, ICacheService cacheService, UserManager<IdentityUser<Guid>> userManager)
        {
            _logger = logger;
            _context = context;
            _cacheService = cacheService;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Sportsman>> GetSportsmans()
        {
            const string key = "all-sportsmans";
            var cached = _cacheService.GetValue<List<Sportsman>>(key);

            if (cached == null)
            {
                var actual = _context.Sportsmans.ToList();
                if (actual.ToList().Count != 0)
                {
                    _cacheService.SetValue(key, actual);
                }
                return actual;
            }
            return cached;
        }

        public async Task<IEnumerable<Competition>> GetCompetitions()
        {
            const string key = "all-competitions";
            var cached = _cacheService.GetValue<List<Competition>>(key);

            if (cached == null)
            {
                var actual = _context.Competition.ToList();
                if (actual.ToList().Count != 0)
                {
                    _cacheService.SetValue(key, actual);
                }
                return actual;
            }
            return cached;
        }

        public async Task AddSportsman(Sportsman sportsman, string username, string email, string password)
        {
            const string key = "all-sportsmans";
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                sportsman.User = new IdentityUser<Guid>
                {
                    UserName = username,
                    Email = email
                };
                var result = await _userManager.CreateAsync(sportsman.User, $"{password}");
                await _userManager.AddToRoleAsync(sportsman.User, Role.Sportsman.ToString());

                _context.Sportsmans.Add(sportsman);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogInformation(ex.Message);
            }

            await transaction.CommitAsync();
            _cacheService.UpdateValue(key);
        }

        public async Task UpdateSportsman(Sportsman sportsman)
        {
            const string key = "all-sportsmans";
            using var transaction = _context.Database.BeginTransaction();
            _context.Update(sportsman);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            _cacheService.UpdateValue(key);
        }

        public async Task DeleteSportsman(Guid id)
        {
            const string key = "all-sportsmans";

            var sportsman = await _context.Sportsmans.FirstOrDefaultAsync(s => s.Id == id);

            if (sportsman == null)
            {
                return;
            }

            _context.Remove(sportsman);
            await _context.SaveChangesAsync();

            _cacheService.UpdateValue(key);
            return;
        }
    }
}
