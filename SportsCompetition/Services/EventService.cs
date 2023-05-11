﻿using Microsoft.AspNetCore.Mvc;
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

        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            const string key = "all-events";
            var cached = _cacheService.GetValue<List<Event>>(key);

            if (cached == null)
            {
                var actual = _context.Event.ToList();
                if (actual.ToList().Count != 0)
                {
                    _cacheService.SetValue(key, actual);
                }
                return actual.AsQueryable();
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
            catch (Exception)
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
            catch (Exception)
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

        public async Task<Event> InitializationOfEvent(Event @event, List<Models.Stream> list)
        {
            @event.Shedule = list;
            @event.CurrentStream = list.First().Id;

            await _context.SaveChangesAsync();
            return @event;
        }

        public async Task<int> SetWeight(SportsmanCompetition sportsmanCompetition, int attemptNumber, int weight)
        {
            var sc = _context.SportsmanCompetition
                .Include(sc => sc.Attempts)
                .FirstOrDefault(sc => sc.Id == sportsmanCompetition.Id);
            try
            {
                if (sc == null)
                {
                    new Exception("sportsmanCompetition is not exist");
                }
            }
            catch (Exception exception)
            {
                _logger.LogInformation(exception.Message);
            }

            sc.Attempts
                .First(a => a.Number == attemptNumber).Weihgt = weight;
            return weight;
        }
        public async Task<int> SetWeight(Guid sportsmanCompetition, int attemptNumber, int weight)
        {
            var sc = _context.SportsmanCompetition
                .Include(sc => sc.Attempts)
                .FirstOrDefault(sc => sc.Id == sportsmanCompetition);



            var att = sc.Attempts
                .FirstOrDefault(a => a.Number == attemptNumber);

            att.Weihgt = weight;

            await _context.SaveChangesAsync();
            return att.Weihgt;
        }
        public async Task<string> SetAttemptResult(Guid sportsmanCompetitionId, int attemptNumber)
        {
            var sportsmanCompetition = _context.SportsmanCompetition
                .Include(sc => sc.Attempts)
                .First(sc => sc.Id == sportsmanCompetitionId);

            if (sportsmanCompetition.Attempts.Count <= attemptNumber)
            {
                var count = sportsmanCompetition.Attempts.Count;
                var @event = sportsmanCompetition.Attempts.First().EventId;

                while (count != attemptNumber - 1)
                {
                    sportsmanCompetition.Attempts.Add(new Attempt()
                    {
                        Number = count + 1,
                        EventId = @event
                    });
                }
            }

            var attempt = sportsmanCompetition.Attempts.FirstOrDefault(a => a.Number == attemptNumber);

            if (attempt.Decisions.Count() < 3)
            {
                return "Some judges don't make a decision";
            }

            if (attempt.Decisions.Count() == 3)
            {
                var trues = 0;
                var falses = 0;
                foreach (var decision in attempt.Decisions)
                {
                    if (decision.JudgeDecision == true)
                    {
                        trues += 1;
                    }
                    else
                    {
                        falses += 1;
                    }
                }

                if (trues > falses)
                {
                    attempt.AttemptResult = true;
                }
            }
            return "result changed";
        }
        public async Task<string> JudgeDesigion(Guid sportsmanCompetitionId, int attemptNumber, Guid judgeId, bool judgeDesigion)
        {
            var sportsmanCompetition = _context.SportsmanCompetition
                .Include(sc => sc.Attempts)
                .Include(sc => sc.Stream)
                .First(sc => sc.Id == sportsmanCompetitionId);

            var judgesOfStream = sportsmanCompetition.Stream.Employees;
            var judge = _context.Employees.First(j => j.Id == judgeId);

            if (!judgesOfStream.Contains(judge))
            {
                return "Wrong Judge!";
            }

            var decisions = sportsmanCompetition.Attempts
                .First(a => a.Number == attemptNumber)
                .Decisions;

            decisions.ToList().Add(new() 
            {
                JudgeDecision = judgeDesigion, Attempt = sportsmanCompetition.Attempts
                .First(a => a.Number == attemptNumber)
            });
            await _context.SaveChangesAsync();
            return "Desigion Added!";
        }
    }
}
