using AutoMapper;
using FluentValidation;
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
    [CustomAuthorize(Role.Judge, Role.Assistant)]
    public class JudgeAndAssistantController : ControllerBase
    {
        private readonly ILogger<SportsmanController> _logger;
        private readonly SportCompetitionDbContext _context;
        private readonly IMapper _mapper;
        private readonly SportsmanCompetitionService _sportsmanCompetitionService;
        private readonly EventService _eventService;
        private readonly StreamService _streamService;

        public JudgeAndAssistantController(
            ILogger<SportsmanController> logger,
            SportCompetitionDbContext context,
            IMapper mapper,
            SportsmanCompetitionService sportsmanCompetitionService,
            EventService eventService,
            StreamService streamService)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _sportsmanCompetitionService = sportsmanCompetitionService;
            _eventService = eventService;
            _streamService = streamService;
        }

        [HttpGet("sportsmanCompetitionAttemptWeight")]
        public async Task<int> GetAtteptWeight(Guid sportsmanCompetition, int attempt)
        {
            return await _sportsmanCompetitionService.GetAtteptWeight(sportsmanCompetition, attempt);
        }

        [CustomAuthorize(Role.Judge)]
        [HttpPut("judgeDesigion")]
        public async Task<IActionResult> JudgeDesigion(Guid sportsmanCompetitionId, int attemptNumber, Guid judgeId, bool judgeDesigion)
        {
            return Ok(await _eventService.JudgeDesigion(sportsmanCompetitionId, attemptNumber, judgeId, judgeDesigion));
        }

        [HttpGet("streamSportsmanCompetition")]
        public async Task<ActionResult> GetJudgesToStream(Guid streamId)
        {
            return Ok(await _streamService.GetStreamSportsmanCompetition(streamId));
        }

        [CustomAuthorize(Role.Judge)]
        [HttpGet("judgeStreams")]
        public async Task<ActionResult> streamsForJudge(Guid JudgeId)
        {
            return Ok(await _streamService.GetJudgeStreams(JudgeId));
        }
        
    }
}
