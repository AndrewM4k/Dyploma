using SportsCompetition.Models;

namespace SportsCompetition.Dtos
{
    public class GetSportsmanCompetitionDto
    {
        public Guid Id { get; set; }
        public Guid SportsmanId { get; set; }
        public Guid CompetitionId { get; set; }
        public ICollection<Attempt> Attempts { get; set; }
    }
}
