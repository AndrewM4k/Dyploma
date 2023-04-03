namespace Migartions.Models
{
    public class Record
    {
        public Guid Id { get; set; }
        public Competition Competition { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string WeightOfSportsman { get; set; }
        public string TypeOfRecord { get; set; }
        public string WeightStandart { get; set; }
    }
}
