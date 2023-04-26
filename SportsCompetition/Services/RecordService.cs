using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SportsCompetition.Controllers;
using SportsCompetition.Dtos;
using SportsCompetition.Models;
using SportsCompetition.Persistance;
using System.Linq;
using WebApplication1.Cache;

namespace SportsCompetition.Services
{
    public class RecordService
    {
        private readonly ILogger<RecordController> _logger;
        private readonly SportCompetitionDbContext _context;
        private readonly ICacheService _cacheService;

        public RecordService(ILogger<RecordController> logger, SportCompetitionDbContext context, ICacheService cacheService)
        {
            _logger = logger;
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<IQueryable<Record>> GetAllRecords()
        {
            const string key = "all-records";
            var cached = _cacheService.GetValue<IQueryable<Record>>(key);

            if (cached == null)
            {
                var actual = _context.Record;
                if (actual.ToList().Count != 0)
                {
                    _cacheService.SetValue(key, actual);
                }
                return actual;
            }
            return cached;
        }

        public async Task AddRecord(Record record)
        {
            const string key = "all-records";
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                await _context.AddAsync(record);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
            }
            _cacheService.UpdateValue(key);
        }

        public async Task UpdateRecord(Record record)
        {
            const string key = "all-records";
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Update(record);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
            }

            _cacheService.UpdateValue(key);
        }

        public async Task DeleteRecord(Guid id)
        {
            const string key = "all-records";
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var record = await _context.Record.FirstOrDefaultAsync(s => s.Id == id);

                if (record == null)
                {
                    return;
                }

                _context.Remove(record);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
            }
            _cacheService.UpdateValue(key);
        }
    }
}
