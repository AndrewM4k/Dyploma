using SportsCompetition.Enums;

namespace SportsCompetition.Models
{
    public class Streama
    {
        public Guid Id { get; set; }
        public Event Event { get; set; }
        public string Name { get; set; }
        public ICollection<StreamJudgeEmployee> StreamJudgeEmployees { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public int Number { get; set; } = 1;
        public ICollection<SportsmanCompetition> SportsmanCompetitions { get; set; }
    }
}
