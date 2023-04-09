using SportsCompetition.Models;

namespace SportsCompetition.Dtos
{
    public class AddStandartDto
    {
        public string CompetitionName { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string WeightOfSportsman { get; set; }
        public string Category { get; set; }
        public string WeightStandart { get; set; }
    }
}
