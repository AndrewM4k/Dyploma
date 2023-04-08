using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Migartions.Dtos;
using Migartions.Models;
using Migartions.Persistance;
using Microsoft.AspNetCore.Authorization;

namespace Migartions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecordController : ControllerBase
    {

        private readonly ILogger<RecordController> _logger;
        private readonly ComposeApiDbContext _context;
        private readonly IMapper _mapper;

        public RecordController(ILogger<RecordController> logger, ComposeApiDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("readAllRecords")]
        public async Task<IEnumerable<GetRecordDto>> Get()
        {
            return _mapper.ProjectTo<GetRecordDto>(_context.Record);
        }

        [HttpPost("addRecord")]
        public async Task<ActionResult> AddRecord(AddRecordDto dto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var record = _mapper.Map<Record>(dto);

                await _context.AddAsync(record);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500);
            }
        }

        [HttpPut("updateRecord")]
        public async Task<ActionResult> UpdateSportsman(UpdateRecordDto dto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var record = _mapper.Map<Record>(dto);

                _context.Update(record);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500);
            }
        }

        [HttpDelete("deleteRecord/{id:Guid}")]
        public async Task<ActionResult> DeleteRecord(Guid id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var record = await _context.Record.FirstOrDefaultAsync(s => s.Id == id);

                if (record == null)
                {
                    return NotFound();
                }

                _context.Remove(record);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500);
            }
        }
    }
}
