using SportsCompetition.Models;
using SportsCompetition.Persistance;

namespace SportsCompetition.Services
{
    public class SportsmanCompetitionService
    {
        private readonly ILogger<SportsmanCompetitionService> _logger;
        private readonly SportCompetitionDbContext _context;

        public SportsmanCompetitionService(ILogger<SportsmanCompetitionService> logger, SportCompetitionDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<SportsmanCompetition> NextWeight(SportsmanCompetition sportsmanCompetition, int nextweight)
        {
            var newAttempt = new Attempt()
            {
                Weihgt = nextweight,
                Number = sportsmanCompetition.Attempts.Last().Number + 1,
            };
            sportsmanCompetition.Attempts.Add(newAttempt);

            return sportsmanCompetition;
        }

        public async Task<SportsmanCompetition> ChangeWeight(SportsmanCompetition sportsmanCompetition, int nextweight)
        {
            var numberAttempt = sportsmanCompetition.CurrentAttempt;
            var attempt = _context.Attempt.Select(a => a.SportsmanCompetition == sportsmanCompetition);
  

            return sportsmanCompetition;
        }

        public async Task<SportsmanCompetition> AttemptsResult(SportsmanCompetition sportsmanCompetition)
        {
            sportsmanCompetition.Attempts.Last().Number += 1;
            return sportsmanCompetition;
        }
    }
}
