namespace SportsCompetition.Models
{
    public class Decision
    {
        public Guid Id { get; set; }
        public bool JudgeDecision { get; set; }
        public Attempt Attempt { get; set; }
    }
}
