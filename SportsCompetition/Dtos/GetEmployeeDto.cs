namespace SportsCompetition.Dtos
{
    public class GetEmployeeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Specialization { get; set; }
    }
}
