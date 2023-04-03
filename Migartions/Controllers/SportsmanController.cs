using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Migartions.Dtos;
using Migartions.Models;
using Migartions.Persistance;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Migartions.Controllers
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

        [HttpGet("ReadAllSportsmens")]
        public async Task<IEnumerable<GetSportsmanDto>> Get()
        {
            return _mapper.ProjectTo<GetSportsmanDto>(_context.Sportsmens);
        }

        [HttpPost("AddSportsmen")]
        public async Task<ActionResult> AddSportsmen(AddSportsmanDto dto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var sportsman = _mapper.Map<Sportsman>(dto);
              
                await _context.AddAsync(sportsman);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                var linesCount = await _context.SaveChangesAsync();

                return Ok(linesCount >= 1);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500);
            }
        }
    }
}
