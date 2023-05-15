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
    public class StandartService
    {
        private readonly ILogger<StandartController> _logger;
        private readonly SportCompetitionDbContext _context;
        private readonly ICacheService _cacheService;

        public StandartService(ILogger<StandartController> logger, SportCompetitionDbContext context, ICacheService cacheService)
        {
            _logger = logger;
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<Standart>> GetAllStandarts()
        {
            const string key = "all-standarts";
            var cached = _cacheService.GetValue<List<Standart>>(key);

            if (cached == null)
            {
                var actual = await _context.Standart.ToListAsync();
                if (actual.ToList().Count != 0)
                {
                    _cacheService.SetValue(key, actual);
                }
                return actual.AsQueryable();
            }
            return cached;
        }

        public async Task AddStandart(Standart standart)
        {
            const string key = "all-standarts";
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                await _context.AddAsync(standart);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }

            _cacheService.UpdateValue(key);
        }

        [HttpPut("updateStandart")]
        public async Task UpdateStandart(Standart standart)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Update(standart);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
            }
        }

        [HttpDelete("deleteStandart/{id:Guid}")]
        public async Task DeleteStandart(Guid id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var standart = await _context.Standart.FirstOrDefaultAsync(s => s.Id == id);

                if (standart == null)
                {
                    return;
                }

                _context.Remove(standart);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
        }
    }
}
