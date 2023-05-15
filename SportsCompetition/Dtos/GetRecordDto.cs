using SportsCompetition.Enums;

namespace SportsCompetition.Dtos
{
    public class GetRecordDto
    {
        public Guid Id { get; set; }
        public string CompetitionName { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public int WeightOfSportsman { get; set; }
        public string TypeOfRecord { get; set; }
        public int WeightStandart { get; set; }
    }
}
