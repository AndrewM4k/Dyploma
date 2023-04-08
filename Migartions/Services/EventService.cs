using Migartions.Models;
using Migartions.Persistance;

namespace Migartions.Services
{
    public class EventService
    {
        private readonly ILogger<EventService> _logger;
        private readonly ComposeApiDbContext _context;

        public EventService(ILogger<EventService> logger, ComposeApiDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Event> CreationEvent(Event evente, List<Streama> list)
        {
            evente.Shedule = list;

            await _context.SaveChangesAsync();
            return evente;
        }
    }
}
