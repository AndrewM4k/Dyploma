using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SportsCompetition.Dtos;
using SportsCompetition.Models;
using SportsCompetition.Persistance;
using System.IO;
using System.Runtime.Serialization;
using WebApplication1.Cache;

namespace SportsCompetition.Services
{
    public class StreamService
    {
        private readonly ILogger<StreamService> _logger;
        private readonly SportCompetitionDbContext _context;
        private readonly ICacheService _cacheService;


        public StreamService(ILogger<StreamService> logger, SportCompetitionDbContext context, ICacheService cacheService)
        {
            _logger = logger;
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<Models.Stream>> GetStreams()
        {
            const string key = "all-Streams";
            var cached = _cacheService.GetValue<List<Models.Stream>>(key);

            if (cached == null)
            {
                var actual = await _context.Streams.ToListAsync();

                if (actual.Count != 0)
                {
                    _cacheService.SetValue(key, actual);
                }

                return actual;
            }
            return cached;
        }

        public async Task AddStream(Models.Stream stream, Guid eventid)
        {
            const string key = "all-Streams";
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var @event = _context.Event.FirstOrDefault(e => e.Id == eventid);
                stream.Event = @event;
                await _context.AddAsync(stream);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
            _cacheService.UpdateValue(key);
        }
        public async Task UpdateStream(Models.Stream stream)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Update(stream);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
            }
        }
        public async Task DeleteStream(Guid id)
        {
            const string key = "all-events";
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var stream = await _context.Streams.FirstOrDefaultAsync(s => s.Id == id);

                if (stream == null)
                {
                    return;
                }

                _context.Remove(stream);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
            }
            _cacheService.UpdateValue(key);
        }

        public async Task<Models.Stream> CreationStream(Guid[] sportsmanCompetitions, Guid @event, Guid streamid, int numberofStream)
        {
            var stream = _context.Streams.FirstOrDefault(s => s.Id == streamid);
            if (stream == null)
            {
                stream = new Models.Stream();
            }
            var list = new List<SportsmanCompetition>();
            foreach (var item in sportsmanCompetitions)
            {
                list.Add(_context.SportsmanCompetition.FirstOrDefault(sc => sc.Id == item));
            }
            
            stream.SportsmanCompetitions = list;
            stream.Event = _context.Event.FirstOrDefault(s => s.Id == @event);
            stream.Number = numberofStream;

            foreach (var item in stream.SportsmanCompetitions)
            {
                var att1 = new Attempt() { Number = 1, EventId = stream.Event.Id };
                var att2 = new Attempt() { Number = 2, EventId = stream.Event.Id };
                var att3 = new Attempt() { Number = 3, EventId = stream.Event.Id };

                var attempts = new List<Attempt>() { att1, att2, att3 };

                item.CurrentAttempt = 1;

                foreach (var attempt in attempts)
                {
                    item.Attempts.Add(attempt);
                }

                await _context.AddAsync(stream);
                await _context.SaveChangesAsync();
            }
            return stream;
        }

        public async Task CreationSportsmanCompetition(Guid sportsmanId, Guid competitionId, Guid streamId)
        {
            try
            {
                var sportsmanCompetition = new SportsmanCompetition()
                {
                    SportsmanId = sportsmanId,
                    CompetitionId = competitionId,
                    CurrentAttempt = 1,
                    Stream = _context.Streams.FirstOrDefault(s=>s.Id == streamId)
                };

                await _context.SportsmanCompetition.AddAsync(sportsmanCompetition);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
            return;
        }

        public async Task<Models.Stream> AddJudgesToStream(Guid streamId, List<Guid> judges)
        {
            var stream = _context.Streams
                .Include(s => s.Employees)
                .FirstOrDefault(s => s.Id == streamId);
            try
            {
                foreach (var judge in judges)
                {

                    var employee = _context.Employees.FirstOrDefault(s => s.Id == judge);
                    if (employee.Role != Enums.Role.Judge)
                    {
                        new Exception("wrong employee, not judge");
                    }
                    employee.Streams.Add(stream);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            await _context.SaveChangesAsync();

            return stream;
        }
    }
}
