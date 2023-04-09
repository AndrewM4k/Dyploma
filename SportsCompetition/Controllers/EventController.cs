using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsCompetition.Dtos;
using SportsCompetition.Models;
using SportsCompetition.Persistance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace SportsCompetition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly ComposeApiDbContext _context;
        private readonly IMapper _mapper;

        public EventController(ILogger<EventController> logger, ComposeApiDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("readAllEvents")]
        public async Task<IEnumerable<GetEventDto>> Get()
        {
            return _mapper.ProjectTo<GetEventDto>(_context.Event);
        }

        [HttpPost("addEvent")]
        public async Task<ActionResult> AddEvent(AddEventDto dto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var @event = _mapper.Map<Event>(dto);

                await _context.AddAsync(@event);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500);
            }
        }

        [HttpPut("updateEvent")]
        public async Task<ActionResult> UpdateEvent(UpdateEventDto dto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var @event = _mapper.Map<Event>(dto);

                _context.Update(@event);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500);
            }
        }

        [HttpDelete("deleteEvent/{id:Guid}")]
        public async Task<ActionResult> DeleteEvent(Guid id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var @event = await _context.Event.FirstOrDefaultAsync(s => s.Id == id);

                if (@event == null)
                {
                    return NotFound();
                }

                _context.Remove(@event);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500);
            }
        }
    }
}
