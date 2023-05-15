using SportsCompetition.Models;

namespace SportsCompetition.Dtos
{
    public class AddRecordDto
    {
        public Enums.Competitions Competition { get; set; }
        public int Age { get; set; }
        public Enums.Gender Gender { get; set; }
        public int WeightOfSportsman { get; set; }
        public string TypeOfRecord { get; set; }
        public int WeightStandart { get; set; }
    }
}
