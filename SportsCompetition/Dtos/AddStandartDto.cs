using SportsCompetition.Models;

namespace SportsCompetition.Dtos
{
    public class AddStandartDto
    {
        public Enums.Competitions CompetitionName { get; set; }
        public int Age { get; set; }
        public Enums.Gender Gender { get; set; }
        public int WeightOfSportsman { get; set; }
        public string Category { get; set; }
        public int WeightStandart { get; set; }
    }
}
