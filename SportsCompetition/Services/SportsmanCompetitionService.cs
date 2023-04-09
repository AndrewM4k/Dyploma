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

        public async Task<Models.SportsmanCompetition> AttemptsResult(Models.SportsmanCompetition sportsmanCompetition, int nextweight)
        {
            sportsmanCompetition.Attempts.Last().Number += 1;
            return sportsmanCompetition;
        }
    }
}
