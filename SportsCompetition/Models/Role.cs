namespace SportsCompetition.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Role(string name) => Name = name;
        public ICollection<Employee> Employees { get; set; }
    }
}
