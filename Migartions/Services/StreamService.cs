﻿using Microsoft.Extensions.Logging;
using Migartions.Models;
using Migartions.Persistance;

namespace Migartions.Services
{
    public class StreamService
    {
        private readonly ILogger<StreamService> _logger;
        private readonly ComposeApiDbContext _context;

        public StreamService(ILogger<StreamService> logger, ComposeApiDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Streama> CreationStream(List<SportsmanCompetition> list, Event evente, int number)
        {
            var streama = new Streama();
            streama.SportsmanCompetitions = list;
            streama.EventId = evente.Id;
            streama.Number = number;

            await _context.SaveChangesAsync();
            return streama;
        }
    }
}
