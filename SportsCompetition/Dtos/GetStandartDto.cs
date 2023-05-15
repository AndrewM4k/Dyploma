using SportsCompetition.Enums;

namespace SportsCompetition.Dtos
{
    public class GetStandartDto
    {
        public Guid Id { get; set; }
        public string CompetitionName { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public int WeightOfSportsman { get; set; }
        public string Category { get; set; }
        public int WeightStandart { get; set; }
    }
}
