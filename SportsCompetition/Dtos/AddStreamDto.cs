using SportsCompetition.Models;

namespace SportsCompetition.Dtos
{
    public class AddStreamDto
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
    }
}
