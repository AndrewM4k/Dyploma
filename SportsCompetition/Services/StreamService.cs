using Microsoft.EntityFrameworkCore;
using SportsCompetition.Models;
using SportsCompetition.Persistance;
using System.IO;

namespace SportsCompetition.Services
{
    public class StreamService
    {
        private readonly ILogger<StreamService> _logger;
        private readonly SportCompetitionDbContext _context;

        public StreamService(ILogger<StreamService> logger, SportCompetitionDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Streama> CreationStream(List<Sportsman> sportsmans, List<Competition> competitions, Event @event, int numberofStream)
        {
            var streama = new Streama();
            var list = new List<SportsmanCompetition>();

            if (sportsmans.Count == competitions.Count)
            {
                for (int i = 0; i < sportsmans.Count; i++)
                {
                    list.Add(await CreationSportsmanCompetition(sportsmans[i], competitions[i]));
                }
            }
            else
            {
                _logger.LogWarning("Not right count of sportsmans and competition");
            }


            streama.SportsmanCompetitions = list;
            streama.Event = @event;
            streama.Number = numberofStream;

            foreach (var item in streama.SportsmanCompetitions)
            {
                var att1 = new Attempt() { Number = 1, EventId = streama.Event.Id };
                var att2 = new Attempt() { Number = 2, EventId = streama.Event.Id };
                var att3 = new Attempt() { Number = 3, EventId = streama.Event.Id };

                var attempts = new List<Attempt>() { att1, att2, att3 };

                item.CurrentAttempt = att1.Id;

                foreach (var attempt in attempts)
                {
                    item.Attempts.Add(attempt);
                }

                await _context.AddAsync(streama);
                await _context.SaveChangesAsync();
            }
            return streama;
        }

        public async Task<SportsmanCompetition> CreationSportsmanCompetition(Sportsman sportsman, Competition competition)
        {
            var sportsmanCompetition = new SportsmanCompetition();

            sportsmanCompetition.SportsmanId = sportsman.Id;
            sportsmanCompetition.CompetitionId = competition.Id;

            await _context.AddAsync(sportsmanCompetition);
            await _context.SaveChangesAsync();
            return sportsmanCompetition;
        }

        public async Task<Streama> AddJudgesToStream(Guid streamId, List<Employee> judges)
        {
            var stream = _context.Streams
                .Include(s => s.Employees)
                .FirstOrDefault(s => s.Id == streamId);

            foreach (var judge in judges)
            {
                judge.Streams.Add(stream);
            }

            await _context.SaveChangesAsync();

            return stream;
        }


    }
}
