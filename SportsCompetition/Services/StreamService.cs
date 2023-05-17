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

                if (@event == null)
                {
                    new Exception("event not exist");
                }

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
            catch (Exception)
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
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
            _cacheService.UpdateValue(key);
        }

        public async Task<Models.Stream> CreationStream(Guid @event, Guid streamid, int numberofStream)
        {
            var stream = _context.Streams
                .Include(s=>s.Event)
                .FirstOrDefault(s => s.Id == streamid);
            try
            {
                var sportsmanCompetitions = await GetStreamSportsmanCompetition(streamid);
                var list = new List<SportsmanCompetition>();
                foreach (var item in sportsmanCompetitions)
                {
                    var sc = _context.SportsmanCompetition.FirstOrDefault(sc => sc.Id == item);
                    if (sc == null)
                    {
                        _logger.LogInformation("SportsmanCompetition is no exist");
                    }

                    if (sc != null)
                    {
                        list.Add(sc);
                    }
                }

                stream.SportsmanCompetitions = list;
                if (stream.Event == null)
                {
                    stream.Event = _context.Event.FirstOrDefault(s => s.Id == @event);
                }
                stream.Number = numberofStream;

                foreach (var item in stream.SportsmanCompetitions)
                {
                    var att1 = new Attempt() { Number = 1, EventId = @event };
                    var att2 = new Attempt() { Number = 2, EventId = @event };
                    var att3 = new Attempt() { Number = 3, EventId = @event };

                    var attempts = new List<Attempt>() { att1, att2, att3 };

                    item.CurrentAttempt = 1;

                    item.Attempts = attempts;

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return stream;
        }

        public async Task CreationSportsmanCompetition(Guid sportsmanId, Guid competitionId, Guid streamId)
        {
            const string key = "all-sportsmanCompetitions";
            try
            {
                var sportsmanCompetition = new SportsmanCompetition()
                {
                    SportsmanId = sportsmanId,
                    CompetitionId = competitionId,
                    CurrentAttempt = 1,
                    Stream = _context.Streams.FirstOrDefault(s => s.Id == streamId),
                    Attempts = new List<Attempt>()
                };

                var result = await _context.SportsmanCompetition.AddAsync(sportsmanCompetition);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            _cacheService.UpdateValue(key);
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
                    stream.Employees.Add(employee);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            await _context.SaveChangesAsync();

            return stream;
        }
        public async Task<IEnumerable<Guid>> GetJudgesToStream(Guid streamId)
        {
            var stream = await _context.Streams
               .Include(s => s.Employees)
               .FirstOrDefaultAsync(s => s.Id == streamId);
            var employeesId = new List<Guid>();
            if (stream == null)
            {
                return employeesId;
            }
            foreach (var judge in stream.Employees) { employeesId.Add(judge.Id); }
            return employeesId;
        }
        public async Task<IEnumerable<Guid>> GetStreamSportsmanCompetition(Guid streamId)
        {
            var stream = await _context.Streams
               .Include(s => s.SportsmanCompetitions)
               .FirstOrDefaultAsync(s => s.Id == streamId);

            var sportsmanCompetitionsId = new List<Guid>();
            if (stream == null)
            {
                return sportsmanCompetitionsId;
            }
            foreach (var judge in stream.SportsmanCompetitions) { sportsmanCompetitionsId.Add(judge.Id); }
            return sportsmanCompetitionsId;
        }

        public async Task<IEnumerable<Guid>> GetJudgeStreams(Guid judgeId)
        {
            var judge = await _context.Employees
               .Include(e => e.Streams)
               .FirstOrDefaultAsync(e => e.Id == judgeId);

            var streamsId = new List<Guid>();
            if (judge.Streams == null)
            {
                return streamsId;
            }

            foreach (var stream in judge.Streams) { streamsId.Add(stream.Id); }
            return streamsId;
        }
    }
}
