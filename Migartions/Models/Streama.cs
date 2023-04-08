using System.ComponentModel.DataAnnotations;

namespace Migartions.Models
{
    public class Streama
    {
        [Key]
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Event Event { get; set; }
        public string Name { get; set; }
        public int Number { get; set; } = 1;
        public ICollection<SportsmanCompetition> SportsmanCompetitions { get; set; }
    }
}
