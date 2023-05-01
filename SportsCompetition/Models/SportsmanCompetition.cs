using System.ComponentModel.DataAnnotations.Schema;

namespace SportsCompetition.Models
{
    public class SportsmanCompetition
    {
        public Guid Id { get; set; }
        public Guid SportsmanId { get; set; }
        public Guid CompetitionId { get; set; }
        public int CurrentAttempt { get; set; }
        public ICollection<Attempt> Attempts { get; set; }
        public Stream Stream { get; set; }
    }
}
