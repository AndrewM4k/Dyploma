using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsCompetition.Dtos;
using SportsCompetition.Models;
using Microsoft.AspNetCore.Authorization;
using SportsCompetition.Persistance;
using SportsCompetition.Services;
using SportsCompetition.Filters;
using SportsCompetition.Enums;

namespace SportsCompetition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize(Role.Administrator, Role.Secretary)]
    public class RecordController : ControllerBase
    {

        private readonly ILogger<RecordController> _logger;
        private readonly SportCompetitionDbContext _context;
        private readonly IMapper _mapper;
        private readonly RecordService _recordService;


        public RecordController(
            ILogger<RecordController> logger, 
            SportCompetitionDbContext context, 
            IMapper mapper, 
            RecordService recordService)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _recordService = recordService;
        }

        [HttpGet]
        public async Task<IEnumerable<GetRecordDto>> Get()
        {
            var records = new List<GetRecordDto>();
            try
            {
                foreach (var item in await _recordService.GetAllRecords())
                {
                    records.Add(_mapper.Map<GetRecordDto>(item));
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
            return records;
        }

        [HttpPost]
        public async Task<IActionResult> AddRecord(AddRecordDto dto)
        {
            var record = _mapper.Map<Record>(dto);
            await _recordService.AddRecord(record);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRecord(UpdateRecordDto dto)
        {
            var record = _mapper.Map<Record>(dto);
            await _recordService.UpdateRecord(record);
            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteRecord(Guid id)
        {
            await _recordService.DeleteRecord(id);
            return Ok();
        }
    }
}
