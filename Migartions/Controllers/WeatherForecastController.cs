using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Migartions.Models;
using Migartions.Persistance;

namespace Migartions.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ComposeApiDbContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ComposeApiDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetSportsmens")]
        public async Task <IEnumerable<Sportsmen>> Get()
        {
            return await _context.Sportsmens.ToListAsync();
        }

        [HttpPost("addSportsmen")]
        public async Task AddSportsmen()
        {
            for (int i = 0; i < 10; i++)
            {
                await _context.AddAsync
                    (new Sportsmen
                     {
                        Name ="pip"+i,
                        Surname = "pip"+i,
                        Gender = "male"
                     });
            }
            await _context.SaveChangesAsync();
        }
    }
}