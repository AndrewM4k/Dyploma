using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsCompetition.Dtos;
using SportsCompetition.Models;
using SportsCompetition.Enums;
using SportsCompetition.Services;
using SportsCompetition.Persistance;
using SportsCompetition.Filters;
using Microsoft.AspNetCore.Authorization;

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
        private readonly StreamService _streamService;
        private readonly SportsmanCompetitionService _sportsmanCompetitionService;

        public SportsmanController(ILogger<SportsmanController> logger, SportCompetitionDbContext context, IMapper mapper, StreamService streamService, SportsmanService sportsmanService, SportsmanCompetitionService sportsmanCompetitionService)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _streamService = streamService;
            _sportsmanService = sportsmanService;
            _sportsmanCompetitionService = sportsmanCompetitionService;
        }

        [HttpGet("readAllSportsmans")]
        public async Task<IEnumerable<GetSportsmanDto>> GetSportsmans()
        {
            var sportsmans = new List<GetSportsmanDto>();
            try
            {
                foreach (var item in await _sportsmanService.GetSportsmans())
                {
                    sportsmans.Add(_mapper.Map<GetSportsmanDto>(item));
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
            return sportsmans;
        }

        [HttpGet("readAllCompetitions")]
        public async Task<IEnumerable<GetCompetitionDto>> GetCompetitions()
        {
            var competitions = new List<GetCompetitionDto>();
            try
            {
                foreach (var item in await _sportsmanService.GetCompetitions())
                {
                    competitions.Add(_mapper.Map<GetCompetitionDto>(item));
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
            return competitions;
        }

        [HttpPost("addSportsman")]
        public async Task<ActionResult> AddSportsman(AddSportsmanDto dto)
        {
            var sportsman = _mapper.Map<Sportsman>(dto);
            var username = dto.Username;
            var email = dto.Email;
            var password = dto.Password;
            await _sportsmanService.AddSportsman(sportsman, username, email, password);

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

        [HttpPost("addSportsmanCompetition")]
        public async Task<ActionResult> AddSportsmanCompetition(Guid sportsmanId, Guid competitionId, Guid streamId)
        {
            await _streamService.CreationSportsmanCompetition(sportsmanId, competitionId, streamId);
            return Ok();
        }

        [HttpGet("getSportsmanCompetitions")]
        public async Task<IEnumerable<GetSportsmanCompetitionDto>> GetSportsmanCompetitions()
        {
            var sportsmanCompetitions = new List<GetSportsmanCompetitionDto>();
            try
            {
                foreach (var item in await _sportsmanCompetitionService.GetSportsmanCompetitions())
                {
                    sportsmanCompetitions.Add(_mapper.Map<GetSportsmanCompetitionDto>(item));
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
            return sportsmanCompetitions;
        }
    }
}
