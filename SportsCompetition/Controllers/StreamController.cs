using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SportsCompetition.Dtos;
using SportsCompetition.Enums;
using SportsCompetition.Filters;
using SportsCompetition.Models;
using SportsCompetition.Persistance;
using SportsCompetition.Services;
using System.Collections.Generic;
using System.IO;

namespace SportsCompetition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[CustomAuthorize(Role.Secretary)]
    //[CustomAuthorize(Role.Administrator)]
    [Authorize]
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

        [HttpGet("readAllStreams")]
        public async Task<IEnumerable<GetStreamDto>> Get()
        {
            var streams = new List<GetStreamDto>();
            try
            {
                foreach (var item in await _streamService.GetStreams())
                {
                    streams.Add(_mapper.Map<GetStreamDto>(item));
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
            return streams;
        }

        [HttpPut("updateStream")]
        public async Task<ActionResult> UpdateStream(UpdateStreamDto dto)
        {
            var stream = _mapper.Map<Models.Stream>(dto);

            _streamService.UpdateStream(stream);
            return Ok();
        }

        [HttpDelete("deleteStream/{id:Guid}")]
        public async Task<ActionResult> DeleteStream(Guid id)
        {
            _streamService.DeleteStream(id);
            return Ok();
        }

        [HttpPost("AddStream")]
        public async Task<ActionResult> AddStream(AddStreamDto dto)
        {
            var stream = _mapper.Map<Models.Stream>(dto);
            var eventid = dto.EventId;
            await _streamService.AddStream(stream, eventid);
            return Ok();
        }

        [HttpPost("creationOfStream")]
        public async Task<ActionResult> CreationStream(Guid[] sportsmanCompetitions, Guid @event, Guid streamId, int numberofStream)
        {
            await _streamService.CreationStream(sportsmanCompetitions, @event, streamId, numberofStream);
            return Ok();
        }

        [HttpPost("addJudgesToStream")]
        public async Task<ActionResult> AddJudgesToStream(Guid streamId, List<Guid> judges)
        {
            await _streamService.AddJudgesToStream(streamId, judges);
            return Ok();
        }
            
    }
}
