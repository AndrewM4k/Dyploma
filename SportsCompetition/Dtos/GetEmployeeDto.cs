using SportsCompetition.Models;

namespace SportsCompetition.Dtos
{
    public class GetEmployeeDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
        public Guid Id { get; set; }
    }
}
