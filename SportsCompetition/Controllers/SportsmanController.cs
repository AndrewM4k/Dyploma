using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsCompetition.Dtos;
using SportsCompetition.Models;
using Microsoft.AspNetCore.Authorization;
using SportsCompetition.Services;
using SportsCompetition.Persistance;

namespace SportsCompetition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SportsmanController : ControllerBase
    {
        private readonly ILogger<SportsmanController> _logger;
        private readonly SportCompetitionDbContext _context;
        private readonly IMapper _mapper;
        private readonly SportsmanService _sportsmanService;

        public SportsmanController(ILogger<SportsmanController> logger, SportCompetitionDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("readAllSportsmans")]
        public async Task<IEnumerable<GetSportsmanDto>> Get()
        {
            return _mapper.ProjectTo<GetSportsmanDto>(await _sportsmanService.GetSportsmans());
        }

        [HttpPost("addSportsman")]
        public async Task<ActionResult> AddSportsman(AddSportsmanDto dto)
        {
            var sportsman = _mapper.Map<Sportsman>(dto);

            _sportsmanService.AddSportsman(sportsman);

            return Ok();
        }

        [HttpPut("updateSportsman")]
        public async Task<ActionResult> UpdateSportsman(UpdateSportsmanDto dto)
        {
            var sportsman = _mapper.Map<Sportsman>(dto);

            _sportsmanService.UpdateSportsman(sportsman);

            return Ok();
        }

        [HttpDelete("deleteSportsman/{id:Guid}")]
        public async Task<ActionResult> DeleteSportsman(Guid id)
        {

            return Ok();
        }
    }
}
