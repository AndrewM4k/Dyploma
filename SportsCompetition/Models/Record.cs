namespace SportsCompetition.Models
{
    public class Record
    {
        public Guid Id { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string WeightOfSportsman { get; set; }
        public string TypeOfRecord { get; set; }
        public string RecordResult { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<Competition> Competitions { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<CompetitionRecord> CompetitionRecords { get; set; }
    }
}
