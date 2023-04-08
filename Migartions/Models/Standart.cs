namespace Migartions.Models
{
    public class Standart
    {
        public Guid Id { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string WeightOfSportsman { get; set; }
        public string Category { get; set; }
        public string StandartResult { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<Competition> Competitions { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<CompetitionStandart> CompetitionStandarts { get; set; }
    }
}
