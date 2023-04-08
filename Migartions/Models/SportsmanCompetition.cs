using System.ComponentModel.DataAnnotations.Schema;

namespace Migartions.Models
{
    public class SportsmanCompetition
    {
        public Guid SportsmanId { get; set; }
        public Guid CompetitionId { get; set; }
        public ICollection<Attempt> Attempts { get; set; }
        public Streama Stream { get; set; }
    }
}
