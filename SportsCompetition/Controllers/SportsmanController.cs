using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsCompetition.Dtos;
using SportsCompetition.Models;
using SportsCompetition.Enums;
using SportsCompetition.Services;
using SportsCompetition.Persistance;
using SportsCompetition.Filters;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
using System.IO;

namespace SportsCompetition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [CustomAuthorize(Role.Administrator, Role.Secretary)]
    public class SportsmanController : ControllerBase
    {
        private readonly ILogger<SportsmanController> _logger;
        private readonly SportCompetitionDbContext _context;
        private readonly IMapper _mapper;
        private readonly SportsmanService _sportsmanService;
        private readonly StreamService _streamService;
        private readonly SportsmanCompetitionService _sportsmanCompetitionService;
        private readonly IValidator<AddSportsmanDto> _validator;

        public SportsmanController(
            ILogger<SportsmanController> logger,
            SportCompetitionDbContext context,
            IMapper mapper, StreamService streamService,
            SportsmanService sportsmanService,
            SportsmanCompetitionService sportsmanCompetitionService, 
            IValidator<AddSportsmanDto> validator)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _streamService = streamService;
            _sportsmanService = sportsmanService;
            _sportsmanCompetitionService = sportsmanCompetitionService;
            _validator = validator;
        }

        [HttpGet]
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

        [HttpGet("competitions")]
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

        [HttpPost]
        public async Task<ActionResult> AddSportsman(AddSportsmanDto dto)
        {

            var validateresult = await _validator.ValidateAsync(dto);
            if (!validateresult.IsValid)
            {
                return BadRequest(validateresult.Errors.ToList());
            }
            var sportsman = _mapper.Map<Sportsman>(dto);
            var username = dto.Username;
            var email = dto.Email;
            var password = dto.Password;
            await _sportsmanService.AddSportsman(sportsman, username, email, password);

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSportsman(UpdateSportsmanDto dto)
        {
            var sportsman = _mapper.Map<Sportsman>(dto);
            await _sportsmanService.UpdateSportsman(sportsman);

            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteSportsman(Guid id)
        {
            await _sportsmanService.DeleteSportsman(id);

            return Ok();
        }

        [HttpPost("sportsmanCompetition")]
        public async Task<ActionResult> AddSportsmanCompetition(Guid sportsmanId, Guid competitionId, Guid streamId)
        {
            await _streamService.CreationSportsmanCompetition(sportsmanId, competitionId, streamId);
            return Ok();
        }

        [HttpGet("sportsmanCompetitions")]
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

        [HttpPut("AttemptsResult")]
        public async Task<ActionResult<bool>> SetAttemptsResult(Guid sportsmanCompetitionId, Status attemptResult, int numberAttempt)
        {
            return Ok(await _sportsmanCompetitionService.SetAttemptsResult(sportsmanCompetitionId, attemptResult, numberAttempt));
        }

        [HttpGet("AttemptsResult")]
        public async Task<ActionResult<string>> GetAttemptsResult(Guid sportsmanCompetitionId, int numberAttempt)
        {
            return Ok(await _sportsmanCompetitionService.GetAttemptsResult(sportsmanCompetitionId, numberAttempt));
        }
    }
}
