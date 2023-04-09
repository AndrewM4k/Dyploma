namespace SportsCompetition.Dtos
{
    public class GetEventDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateofStart { get; set; }
    }
}
