using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsCompetition.Dtos;
using SportsCompetition.Models;
using SportsCompetition.Persistance;

namespace SportsCompetition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StreamController : ControllerBase
    {
        private readonly ILogger<StreamController> _logger;
        private readonly SportCompetitionDbContext _context;
        private readonly IMapper _mapper;

        public StreamController(ILogger<StreamController> logger, SportCompetitionDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("readAllStreams")]
        public async Task<IEnumerable<GetStreamDto>> Get()
        {
            return _mapper.ProjectTo<GetStreamDto>(_context.Streams);
        }

        [HttpPost("addStream")]
        public async Task<ActionResult> AddStream(AddStreamDto dto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var stream = _mapper.Map<Streama>(dto);

                await _context.AddAsync(stream);
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

        [HttpPut("updateStream")]
        public async Task<ActionResult> UpdateStream(UpdateStreamDto dto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var stream = _mapper.Map<Streama>(dto);

                await _context.AddAsync(stream);
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

        [HttpDelete("deleteStream/{id:Guid}")]
        public async Task<ActionResult> DeleteStream(Guid id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var employee = await _context.Streams.FirstOrDefaultAsync(e => e.Id == id);

                if (employee == null)
                {
                    return NotFound();
                }

                _context.Remove(employee);
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
