namespace Migartions.Dtos
{
    public class GetRecordDto
    {
        public Guid Id { get; set; }
        public string CompetitionName { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string WeightOfSportsman { get; set; }
        public string TypeOfRecord { get; set; }
        public string WeightStandart { get; set; }
    }
}
