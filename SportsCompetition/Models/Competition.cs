namespace SportsCompetition.Models
{
    public class Competition
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Equpment { get; set; }

        public ICollection<Event> Events { get; set; }

        public ICollection<EventCompetition> EventCompetitions { get; set; }

        public ICollection<SportsmanCompetition> SportsmanCompetitions { get; set; }

        public ICollection<Sportsman> Sportsmans { get; set; }

        public ICollection<Standart> Standarts { get; set; }

        public ICollection<Record> Records { get; set; }

        public ICollection<CompetitionRecord> CompetitionRecords { get; set; }

        public ICollection<CompetitionStandart> CompetitionStandarts { get; set; }
    }
}
