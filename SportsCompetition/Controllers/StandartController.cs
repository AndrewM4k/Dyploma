using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsCompetition.Dtos;
using SportsCompetition.Models;
using Microsoft.AspNetCore.Authorization;
using SportsCompetition.Persistance;
using SportsCompetition.Services;

namespace SportsCompetition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StandartController : ControllerBase
    {
        private readonly ILogger<StandartController> _logger;
        private readonly SportCompetitionDbContext _context;
        private readonly IMapper _mapper;
        private readonly StandartService _standartService;

        public StandartController(ILogger<StandartController> logger, SportCompetitionDbContext context, IMapper mapper, StandartService standartService)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _standartService = standartService;
        }

        [HttpGet("readAllStandarts")]
        public async Task<IEnumerable<GetStandartDto>> Get()
        {
            return _mapper.ProjectTo<GetStandartDto>(await _standartService.GetAllStandarts());
        }

        [HttpPost("addStandart")]
        public async Task<ActionResult> AddStandart(AddStandartDto dto)
        {
            var standart = _mapper.Map<Standart>(dto);
            _standartService.AddStandart(standart);
            return Ok();
        }

        [HttpPut("updateStandart")]
        public async Task<ActionResult> UpdateStandart(UpdateStandartDto dto)
        {
            var standart = _mapper.Map<Standart>(dto);
            _standartService.UpdateStandart(standart);
            return Ok();
        }

        [HttpDelete("deleteStandart/{id:Guid}")]
        public async Task<ActionResult> DeleteStandart(Guid id)
        {
            _standartService.DeleteStandart(id);
            return Ok();
        }
    }
}
