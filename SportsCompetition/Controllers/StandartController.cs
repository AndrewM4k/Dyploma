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
    public class StandartController : ControllerBase
    {
        private readonly ILogger<StandartController> _logger;
        private readonly ComposeApiDbContext _context;
        private readonly IMapper _mapper;

        public StandartController(ILogger<StandartController> logger, ComposeApiDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("readAllStandarts")]
        public async Task<IEnumerable<GetStandartDto>> Get()
        {
            return _mapper.ProjectTo<GetStandartDto>(_context.Standart);
        }

        [HttpPost("addStandart")]
        public async Task<ActionResult> AddStandart(AddStandartDto dto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var standart = _mapper.Map<Standart>(dto);

                await _context.AddAsync(standart);
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

        [HttpPut("updateStandart")]
        public async Task<ActionResult> UpdateSportsman(UpdateStandartDto dto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var standart = _mapper.Map<Standart>(dto);

                _context.Update(standart);
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

        [HttpDelete("deleteStandart/{id:Guid}")]
        public async Task<ActionResult> DeleteStandart(Guid id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var standart = await _context.Standart.FirstOrDefaultAsync(s => s.Id == id);

                if (standart == null)
                {
                    return NotFound();
                }

                _context.Remove(standart);
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
