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
    public class RecordController : ControllerBase
    {

        private readonly ILogger<RecordController> _logger;
        private readonly SportCompetitionDbContext _context;
        private readonly IMapper _mapper;
        private readonly RecordService _recordService;


        public RecordController(ILogger<RecordController> logger, SportCompetitionDbContext context, IMapper mapper, RecordService recordService)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _recordService = recordService;
        }

        [HttpGet("readAllRecords")]
        public async Task<IEnumerable<GetRecordDto>> Get()
        {
            return _mapper.ProjectTo<GetRecordDto>(await _recordService.GetAllRecords());
        }

        [HttpPost("addRecord")]
        public async Task<IActionResult> AddRecord(AddRecordDto dto)
        {
            var record = _mapper.Map<Record>(dto);
            _recordService.AddRecord(record);
            return Ok();
        }

        [HttpPut("updateRecord")]
        public async Task<IActionResult> UpdateRecord(UpdateRecordDto dto)
        {
            var record = _mapper.Map<Record>(dto);
            _recordService.UpdateRecord(record);
            return Ok();
        }

        [HttpDelete("deleteRecord/{id:Guid}")]
        public async Task<IActionResult> DeleteRecord(Guid id)
        {
            _recordService.DeleteRecord(id);
            return Ok();
        }
    }
}
