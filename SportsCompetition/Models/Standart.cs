using SportsCompetition.Enums;

namespace SportsCompetition.Models
{
    public class Standart
    {
        public Guid Id { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public int WeightOfSportsman { get; set; }
        public string Category { get; set; }
        public int StandartResult { get; set; }

        public ICollection<Competition> Competitions { get; set; }

        public ICollection<CompetitionStandart> CompetitionStandarts { get; set; }
    }
}
