using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsCompetition.Dtos;
using SportsCompetition.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using SportsCompetition.Services;
using SportsCompetition.Persistance;
using SportsCompetition.Filters;
using SportsCompetition.Enums;

namespace SportsCompetition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [CustomAuthorize(Role.Administrator, Role.Secretary)]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly SportCompetitionDbContext _context;
        private readonly IMapper _mapper;
        private readonly EventService _eventService;

        public EventController(
            ILogger<EventController> logger, 
            SportCompetitionDbContext context, 
            IMapper mapper, 
            EventService eventService)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _eventService = eventService;
        }

        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> AddEvent(AddEventDto dto)
        {
            var @event = _mapper.Map<Event>(dto);
            await _eventService.AddEvent(@event);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEvent(UpdateEventDto dto)
        {
            var @event = _mapper.Map<Event>(dto);
            await _eventService.UpdateEvent(@event);

            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            await _eventService.DeleteEvent(id);
            return Ok();
        }

        [HttpPut("attemptResult")]
        public async Task<IActionResult> SetAttemptResult(Guid sportsmanCompetitionId, int attemptNumber)
        {
            return Ok(await _eventService.SetAttemptResult(sportsmanCompetitionId, attemptNumber));
        }

        [HttpPut("judgeDesigion")]
        public async Task<IActionResult> JudgeDesigion(Guid sportsmanCompetitionId, int attemptNumber, Guid judgeId, bool judgeDesigion)
        {
            return Ok(await _eventService.JudgeDesigion(sportsmanCompetitionId, attemptNumber, judgeId, judgeDesigion));
        }
    }
}
