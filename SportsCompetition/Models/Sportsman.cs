namespace SportsCompetition.Models
{
    public class Sportsman
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Birthdate { get; set; }
 
        public ICollection<Competition> Competitions { get; set; }

        public ICollection<SportsmanCompetition> SportsmanCompetitions { get; set; }
        
        public ICollection<Event> Events { get; set; }

        public ICollection<EventSportsman> EventSportsmans { get; set; }
    }
}
