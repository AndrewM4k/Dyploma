using SportsCompetition.Controllers;
using SportsCompetition.Dtos;
using SportsCompetition.Models;
using SportsCompetition.Persistance;

namespace SportsCompetition.Services
{
    public class SportsmanCompetitionService
    {
        private readonly ILogger<SportsmanCompetitionService> _logger;
        private readonly ComposeApiDbContext _context;

        public SportsmanCompetitionService(ILogger<SportsmanCompetitionService> logger, ComposeApiDbContext context)
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
            sportsmanCompetition.Attempts.Last().Weihgt = nextweight;

            return sportsmanCompetition;
        }

        public async Task<SportsmanCompetition> AttemptsResult(SportsmanCompetition sportsmanCompetition, int nextweight)
        {
            sportsmanCompetition.Attempts.Last().Number += 1;
            return sportsmanCompetition;
        }
    }
}
