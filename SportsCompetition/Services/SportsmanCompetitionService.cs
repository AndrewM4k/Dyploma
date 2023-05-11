using Microsoft.EntityFrameworkCore;
using SportsCompetition.Models;
using SportsCompetition.Persistance;
using WebApplication1.Cache;

namespace SportsCompetition.Services
{
    public class SportsmanCompetitionService
    {
        private readonly ILogger<SportsmanCompetitionService> _logger;
        private readonly SportCompetitionDbContext _context;
        private readonly ICacheService _cacheService;

        public SportsmanCompetitionService(ILogger<SportsmanCompetitionService> logger, SportCompetitionDbContext context, ICacheService cacheService)
        {
            _logger = logger;
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<SportsmanCompetition>> GetSportsmanCompetitions()
        {
            const string key = "all-sportsmanCompetitions";
            var cached = _cacheService.GetValue<List<SportsmanCompetition>>(key);

            if (cached == null)
            {
                var actual = _context.SportsmanCompetition.ToList();
                if (actual.ToList().Count != 0)
                {
                    _cacheService.SetValue(key, actual);
                }
                return actual;
            }
            return cached;
        }

        public async Task<int> GetAtteptWeight(Guid sportsmanCompetition, int atempt)
        {
            var sc = _context.SportsmanCompetition
                .Include(sc => sc.Attempts)
                .FirstOrDefault(sc => sc.Id == sportsmanCompetition);
            return sc.Attempts
                .FirstOrDefault(a => a.Number == atempt)
                .Weihgt;
        }

        public async Task<bool> SetAttemptsResult(SportsmanCompetition sportsmanCompetition, bool attemptResult, int numberAttempt)
        {
            if (numberAttempt == 0)
            {
                return false;
            }
            while (sportsmanCompetition
                .Attempts.Count < numberAttempt)
            {
                sportsmanCompetition
                .Attempts
                .Add(new Attempt()
                {
                    Number = sportsmanCompetition.Attempts.Count +1
                });
            }

            try
            {
                sportsmanCompetition.Attempts.FirstOrDefault(a => a.Number == numberAttempt)
                .AttemptResult = attemptResult;

                sportsmanCompetition.CurrentAttempt += 1;

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }
    }
}
