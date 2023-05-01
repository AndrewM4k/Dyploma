using SportsCompetition.Enums;

namespace SportsCompetition.Dtos
{
    public class GetSportsmanDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
