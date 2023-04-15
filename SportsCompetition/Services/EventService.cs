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

        public async Task<Event> CreationEvent(Event @event, List<Streama> list)
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
            sportsmanCompetition.Attempts.num
            return @event;
        }
    }
}
