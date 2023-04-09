using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsCompetition.Dtos;
using SportsCompetition.Models;
using SportsCompetition.Persistance;
using Microsoft.AspNetCore.Authorization;

namespace SportsCompetition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SportsmanController : ControllerBase
    {
        private readonly ILogger<SportsmanController> _logger;
        private readonly ComposeApiDbContext _context;
        private readonly IMapper _mapper;

        public SportsmanController(ILogger<SportsmanController> logger, ComposeApiDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("readAllSportsmans")]
        public async Task<IEnumerable<GetSportsmanDto>> Get()
        {
            return _mapper.ProjectTo<GetSportsmanDto>(_context.Sportsmans);
        }

        [HttpPost("addSportsman")]
        public async Task<ActionResult> AddSportsman(AddSportsmanDto dto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var sportsman = _mapper.Map<Sportsman>(dto);
              
                await _context.AddAsync(sportsman);
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

        [HttpPut("updateSportsman")]
        public async Task<ActionResult> UpdateSportsman(UpdateSportsmanDto dto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var sportsman = _mapper.Map<Sportsman>(dto);

                _context.Update(sportsman);
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

        [HttpDelete("deleteSportsman/{id:Guid}")]
        public async Task<ActionResult> DeleteSportsman(Guid id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var sportsman = await _context.Sportsmans.FirstOrDefaultAsync(s => s.Id == id);

                if (sportsman == null)
                {
                    return NotFound();
                }

                _context.Remove(sportsman);
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
