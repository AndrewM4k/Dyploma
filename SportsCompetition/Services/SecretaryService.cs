﻿using SportsCompetition.Persistance;

namespace SportsCompetition.Services
{
    public class SecretaryService
    {
        private readonly SportsmanCompetitionService _sportsmancompetitionservice;
        private readonly ILogger<EventService> _logger;
        private readonly SportCompetitionDbContext _context;

        public SecretaryService(ILogger<EventService> logger, SportCompetitionDbContext context, SportsmanCompetitionService sportsmancompetitionservice)
        {
            _logger = logger;
            _context = context;
            _sportsmancompetitionservice = sportsmancompetitionservice;
        }


    }
}