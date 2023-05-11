﻿namespace SportsCompetition.Models
{
    public class Attempt
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public int Number { get; set; } = 1;
        public int Weihgt { get; set; }
        public bool AttemptResult { get; set; }
        public IEnumerable<Decision> Decisions { get; set; }
        public SportsmanCompetition SportsmanCompetition { get; set; }
    }
}
