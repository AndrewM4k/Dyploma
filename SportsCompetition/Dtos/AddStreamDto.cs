using SportsCompetition.Models;

namespace SportsCompetition.Dtos
{
    public class AddStreamDto
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
    }
}
