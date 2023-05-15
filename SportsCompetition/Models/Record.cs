using SportsCompetition.Enums;

namespace SportsCompetition.Models
{
    public class Record
    {
        public Guid Id { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public int WeightOfSportsman { get; set; }
        public string TypeOfRecord { get; set; }
        public int RecordResult { get; set; }

        public ICollection<Competition> Competitions { get; set; }

        public ICollection<CompetitionRecord> CompetitionRecords { get; set; }
    }
}
