namespace SportsCompetition.Dtos
{
    public class GetSportsmanDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
