using Microsoft.Extensions.Logging;
using SportsCompetition.Models;
using SportsCompetition.Persistance;

namespace SportsCompetition.Services
{
    public class StreamService
    {
        private readonly ILogger<StreamService> _logger;
        private readonly ComposeApiDbContext _context;

        public StreamService(ILogger<StreamService> logger, ComposeApiDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Streama> CreationStream(List<SportsmanCompetition> list, Event @event, int number)
        {
            var streama = new Streama();
            streama.SportsmanCompetitions = list;
            streama.EventId = @event.Id;
            streama.Number = number;

            await _context.SaveChangesAsync();
            return streama;
        }

        public async Task<SportsmanCompetition> CreationSportsmanCompetition(List<Sportsman> list)
        {
            var streama = new Streama();
            streama.SportsmanCompetitions = list;
            streama.EventId = @event.Id;
            streama.Number = number;

            await _context.SaveChangesAsync();
            return streama;
        }
    }
}
