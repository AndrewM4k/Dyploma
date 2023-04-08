using Migartions.Controllers;
using Migartions.Dtos;
using Migartions.Models;
using Migartions.Persistance;

namespace Migartions.Services
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

        public async Task<SportsmanCompetition> GoodLift(SportsmanCompetition sportsmanCompetition, int nextweight)
        {
            sportsmanCompetition.Attempts.Last().Number += 1;
            return sportsmanCompetition;
        }

        public async Task<SportsmanCompetition> NoLift(SportsmanCompetition sportsmanCompetition, int nextweight)
        {
            sportsmanCompetition.Attempts.Last().Number += 1;
            return sportsmanCompetition;
        }
    }
}
