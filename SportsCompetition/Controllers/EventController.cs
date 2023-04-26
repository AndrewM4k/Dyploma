using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsCompetition.Dtos;
using SportsCompetition.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using SportsCompetition.Services;
using SportsCompetition.Persistance;

namespace SportsCompetition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly SportCompetitionDbContext _context;
        private readonly IMapper _mapper;
        private readonly EventService _eventService;

        public EventController(ILogger<EventController> logger, SportCompetitionDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("readAllEvents")]
        public async Task<IEnumerable<GetEventDto>> Get()
        {
            return _mapper.ProjectTo<GetEventDto>(await _eventService.GetEventsAsync());
        }

        [HttpPost("addEvent")]
        public async Task<ActionResult> AddEvent(AddEventDto dto)
        {
            var @event = _mapper.Map<Event>(dto);
            await _eventService.AddEvent(@event);

            return Ok();
        }

        [HttpPut("updateEvent")]
        public async Task<ActionResult> UpdateEvent(UpdateEventDto dto)
        {
            var @event = _mapper.Map<Event>(dto);
            await _eventService.UpdateEvent(@event);

            return Ok();
        }

        [HttpDelete("deleteEvent/{id:Guid}")]
        public async Task<ActionResult> DeleteEvent(Guid id)
        {
            await _eventService.DeleteEvent(id);
            return Ok();
        }
    }
}
