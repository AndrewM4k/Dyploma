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
    public class SportsmanService
    {
        private readonly ILogger<SportsmanController> _logger;
        private readonly SportCompetitionDbContext _context;
        private readonly ICacheService _cacheService;

        public SportsmanService(ILogger<SportsmanController> logger, SportCompetitionDbContext context, ICacheService cacheService)
        {
            _logger = logger;
            _context = context;
            _cacheService = cacheService;
        }

        [HttpGet("readAllSportsmans")]
        public async Task<IQueryable<Sportsman>> GetSportsmans()
        {
            const string key = "all-sportsmans";
            var cached = _cacheService.GetValue<IQueryable<Sportsman>>(key);

            if (cached == null)
            {
                var actual = _context.Sportsmans;
                if (actual.ToList().Count != 0)
                {
                    _cacheService.SetValue(key, actual);
                }
                return actual;
            }
            return cached;
        }

        public async Task AddSportsman(Sportsman sportsman)
        {
            const string key = "all-sportsmans";
            using var transaction = _context.Database.BeginTransaction();

            await _context.AddAsync(sportsman);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            await transaction.RollbackAsync();
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
