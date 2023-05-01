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

        public EventController(ILogger<EventController> logger, SportCompetitionDbContext context, IMapper mapper, EventService eventService)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _eventService = eventService;
        }

        [HttpGet("readAllEvents")]
        public async Task<IEnumerable<GetEventDto>> Get()
        {
            var events = new List<GetEventDto>();
            try
            {
                foreach (var item in await _eventService.GetEventsAsync())
                {
                    events.Add(_mapper.Map<GetEventDto>(item));
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
            return events;
        }

        [HttpPost("addEvent")]
        public async Task<IActionResult> AddEvent(AddEventDto dto)
        {
            var @event = _mapper.Map<Event>(dto);
            await _eventService.AddEvent(@event);

            return Ok();
        }

        [HttpPut("updateEvent")]
        public async Task<IActionResult> UpdateEvent(UpdateEventDto dto)
        {
            var @event = _mapper.Map<Event>(dto);
            await _eventService.UpdateEvent(@event);

            return Ok();
        }

        [HttpDelete("deleteEvent/{id:Guid}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            await _eventService.DeleteEvent(id);
            return Ok();
        }
    }
}
