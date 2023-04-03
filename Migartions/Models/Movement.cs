namespace Migartions.Models
{
    public class Movement
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<Competition> Competitions { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<MovementCompetition> MovementCompetitions { get; set; }
    }
}
