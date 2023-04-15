
using System.ComponentModel.DataAnnotations;

namespace SportsCompetition.Models
{
    public class Event
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CurrentStream { get; set; }
        public string Description { get; set; }
        public DateTime DateofStart { get; set; }

        public ICollection<Streama> Shedule { get; set;}

        public ICollection<Competition> Competitions { get; set; }

        public ICollection<EventCompetition> EventCompetitions { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public ICollection<EmployeeEvent> EmployeeEvents { get; set; }

        public ICollection<Sportsman> Sportsmans { get; set; }

        public ICollection<EventSportsman> EventSportsmans { get; set; }
    }
}