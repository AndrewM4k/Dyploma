using SportsCompetition.Models;
using SportsCompetition.Persistance;

namespace SportsCompetition.Services
{
    public class EventService
    {
        private readonly SportsmanCompetitionService _sportsmancompetitionservice;
        private readonly ILogger<EventService> _logger;
        private readonly ComposeApiDbContext _context;

        public EventService(ILogger<EventService> logger, ComposeApiDbContext context, SportsmanCompetitionService sportsmancompetitionservice)
        {
            _logger = logger;
            _context = context;
            _sportsmancompetitionservice = sportsmancompetitionservice;
        }

        public async Task<Event> CreationEvent(Event evente, List<Streama> list)
        {
            evente.Shedule = list;

            await _context.SaveChangesAsync();
            return evente;
        }
        public async Task<Event> StartEvent(Event evente)
        {

            foreach (var item1 in evente.Shedule)
            {
                foreach (var item2 in item1.SportsmanCompetitions)
                {
                    int nextweight = 0;
                    Models.SportsmanCompetition sportsmanCompetition = item2;
                    await _sportsmancompetitionservice.AttemptsResult(sportsmanCompetition, nextweight);
                }

                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
            return evente;
        }
    }
}
