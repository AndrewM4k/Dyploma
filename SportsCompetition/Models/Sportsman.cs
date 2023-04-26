using SportsCompetition.Enums;

namespace SportsCompetition.Models
{
    public class Sportsman
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public ICollection<Competition> Competitions { get; set; }

        public ICollection<SportsmanCompetition> SportsmanCompetitions { get; set; }
        
        public ICollection<Event> Events { get; set; }

        public ICollection<EventSportsman> EventSportsmans { get; set; }
    }
}
