using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsCompetition.Dtos;
using SportsCompetition.Enums;
using SportsCompetition.Filters;
using SportsCompetition.Persistance;
using SportsCompetition.Services;

namespace SportsCompetition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize(Role.Administrator, Role.Secretary)]
    public class StreamController : ControllerBase
    {
        private readonly ILogger<StreamController> _logger;
        private readonly SportCompetitionDbContext _context;
        private readonly IMapper _mapper;
        private readonly StreamService _streamService;

        public StreamController(ILogger<StreamController> logger, SportCompetitionDbContext context, IMapper mapper, StreamService streamService)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _streamService = streamService;
        }

        [HttpGet]
        public async Task<IEnumerable<GetStreamDto>> Get()
        {
            var streams = new List<GetStreamDto>();
            try
            {
                streams = _mapper.Map<List<GetStreamDto>>(await _streamService.GetStreams());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
            return streams;
        }

        [HttpPut]
        public async Task<ActionResult> UpdateStream(UpdateStreamDto dto)
        {
            var stream = _mapper.Map<Models.Stream>(dto);

            await _streamService.UpdateStream(stream);
            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteStream(Guid id)
        {
            await _streamService.DeleteStream(id);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> AddStream(AddStreamDto dto)
        {
            var stream = _mapper.Map<Models.Stream>(dto);
            var eventid = dto.EventId;
            await _streamService.AddStream(stream, eventid);
            return Ok();
        }

        [HttpPost("streamInit")]
        public async Task<ActionResult> CreationStream(Guid[] sportsmanCompetitions, Guid @event, Guid streamId, int numberofStream)
        {
            await _streamService.CreationStream(sportsmanCompetitions, @event, streamId, numberofStream);
            return Ok();
        }

        [HttpPost("streamJudjes")]
        public async Task<ActionResult> AddJudgesToStream(Guid streamId, List<Guid> judges)
        {
            await _streamService.AddJudgesToStream(streamId, judges);
            return Ok();
        }

        [HttpPut("streamJudjes")]
        public async Task<ActionResult> SetJudgesToStream(Guid streamId)
        {
            return Ok(await _streamService.SetJudgesToStream(streamId));
        }

        [HttpGet("streamSportsmanCompetition")]
        public async Task<ActionResult> GetJudgesToStream(Guid streamId)
        {
            return Ok(await _streamService.GetStreamSportsmanCompetition(streamId));
        }
    }
}
