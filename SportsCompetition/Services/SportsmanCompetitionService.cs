using Microsoft.EntityFrameworkCore;
using SportsCompetition.Enums;
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
                var actual = await _context.SportsmanCompetition.ToListAsync();
                if (actual.Count != 0)
                {
                    _cacheService.SetValue(key, actual);
                }
                return actual;
            }
            return cached;
        }

        public async Task<int> GetAtteptWeight(Guid sportsmanCompetition, int attempt)
        {
            var sc = await _context.SportsmanCompetition
                .Include(sc => sc.Attempts)
                .FirstOrDefaultAsync(sc => sc.Id == sportsmanCompetition);

            if (sc == null)
            {
                return 0;
            }

            return sc.Attempts
                .First(a => a.Number == attempt)
                .Weihgt;
        }

        public async Task<bool> SetAttemptsResult(Guid sportsmanCompetitionId, Status attemptResult, int numberAttempt)
        {
            if (numberAttempt == 0)
            {
                return false;
            }

            var sportsmanCompetition = _context.SportsmanCompetition
                .Include(sc => sc.Attempts)
                .First(sc => sc.Id == sportsmanCompetitionId);

            while (sportsmanCompetition
                .Attempts.Count < numberAttempt)
            {
                sportsmanCompetition
                .Attempts
                .Add(new Attempt()
                {
                    Number = sportsmanCompetition.Attempts.Count + 1
                });
            }

            try
            {
                sportsmanCompetition.Attempts.First(a => a.Number == numberAttempt)
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

        public async Task<string> GetAttemptsResult(Guid sportsmanCompetitionId, int numberAttempt)
        {
            var sportsmanCompetition = await _context.SportsmanCompetition
                .Include(sc => sc.Attempts)
                .FirstAsync(sc => sc.Id == sportsmanCompetitionId);

            if (numberAttempt == 0 || numberAttempt > sportsmanCompetition.Attempts.Count)
            {
                return "wrong attempt";
            }

            return sportsmanCompetition.Attempts.First(a => a.Number == numberAttempt)
                .AttemptResult.ToString();
        }

        public async Task<int> SetWeight(Guid sportsmanCompetition, int attemptNumber, int weight)
        {
            var sc = _context.SportsmanCompetition
                .Include(sc => sc.Attempts)
                .FirstOrDefault(sc => sc.Id == sportsmanCompetition);

            if (sc == null)
            {
                new Exception("sportsmanCompetition is not exist");
                return 0;
            }

            sc.Attempts
                .First(a => a.Number == attemptNumber)
                .Weihgt = weight;

            await _context.SaveChangesAsync();
            return weight;
        }
    }
}
