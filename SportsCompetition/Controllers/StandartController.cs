using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsCompetition.Dtos;
using SportsCompetition.Models;
using SportsCompetition.Persistance;
using SportsCompetition.Services;
using SportsCompetition.Filters;
using SportsCompetition.Enums;

namespace SportsCompetition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize(Role.Administrator, Role.Secretary)]
    public class StandartController : ControllerBase
    {
        private readonly ILogger<StandartController> _logger;
        private readonly SportCompetitionDbContext _context;
        private readonly IMapper _mapper;
        private readonly StandartService _standartService;

        public StandartController(
            ILogger<StandartController> logger, 
            SportCompetitionDbContext context, 
            IMapper mapper, 
            StandartService standartService)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _standartService = standartService;
        }

        [HttpGet]
        public async Task<IEnumerable<GetStandartDto>> Get()
        {
            var standarts = new List<GetStandartDto>();
            try
            {
                foreach (var item in await _standartService.GetAllStandarts())
                {
                    standarts.Add(_mapper.Map<GetStandartDto>(item));
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
            return standarts;
        }

        [HttpPost]
        public async Task<ActionResult> AddStandart(AddStandartDto dto)
        {
            var standart = _mapper.Map<Standart>(dto);
            await _standartService.AddStandart(standart);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateStandart(UpdateStandartDto dto)
        {
            var standart = _mapper.Map<Standart>(dto);
            await _standartService.UpdateStandart(standart);
            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteStandart(Guid id)
        {
            await _standartService.DeleteStandart(id);
            return Ok();
        }
    }
}
