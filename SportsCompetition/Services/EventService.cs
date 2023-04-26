using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using SportsCompetition.Dtos;
using SportsCompetition.Models;
using SportsCompetition.Persistance;
using WebApplication1.Cache;

namespace SportsCompetition.Services
{
    public class EventService
    {
        private readonly SportsmanCompetitionService _sportsmancompetitionservice;
        private readonly ILogger<EventService> _logger;
        private readonly SportCompetitionDbContext _context;
        private readonly ICacheService _cacheService;

        public EventService(ILogger<EventService> logger, SportCompetitionDbContext context, SportsmanCompetitionService sportsmancompetitionservice, ICacheService cacheService)
        {
            _logger = logger;
            _context = context;
            _sportsmancompetitionservice = sportsmancompetitionservice;
            _cacheService = cacheService;
        }

        public async Task<IQueryable<Event>> GetEventsAsync()
        {
            const string key = "all-events";
            var cached = _cacheService.GetValue<IQueryable<Event>>(key);

            if (cached == null)
            {
                var actual = _context.Event;
                if (actual.ToList().Count != 0)
                {
                    _cacheService.SetValue(key, actual);
                }
                return actual;
            }
            return cached;
        }

        public async Task AddEvent(Event @event)
        {
            const string key = "all-events";
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                await _context.AddAsync(@event);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
            }
            _cacheService.UpdateValue(key);
        }

        public async Task UpdateEvent(Event @event)
        {
            const string key = "all-events";
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Update(@event);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
            }
            _cacheService.UpdateValue(key);
        }
        public async Task DeleteEvent(Guid id)
        {
            const string key = "all-events";
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var @event = await _context.Event.FirstOrDefaultAsync(s => s.Id == id);

                if (@event == null)
                {
                    return;
                }

                _context.Remove(@event);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
            }
            _cacheService.UpdateValue(key);
        }

        public async Task<Event> InitializationOfEvent(Event @event, List<Streama> list)
        {
            @event.Shedule = list;
            @event.CurrentStream = list.First().Id;

            await _context.SaveChangesAsync();
            return @event;
        }
        public async Task<Event> StartEvent(Event @event)
        {
            foreach (var item1 in @event.Shedule)
            {
                foreach (var item2 in item1.SportsmanCompetitions)
                {
                    SportsmanCompetition sportsmanCompetition = item2;
                    await _sportsmancompetitionservice.AttemptsResult(sportsmanCompetition);
                }
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
            return @event;
        }

        public async Task<int> SetWeight(SportsmanCompetition sportsmanCompetition, int attemptNumber, int weight)
        {
            var sc =_context.SportsmanCompetition
                .Include(sc => sc.Attempts)
                .FirstOrDefault(sc => sc.Id == sportsmanCompetition.Id);
            if (sc == null)
            {

            }
            sc.Attempts
                .FirstOrDefault(a => a.Number == attemptNumber).Weihgt = weight;
            return weight;
        }
    }
}
