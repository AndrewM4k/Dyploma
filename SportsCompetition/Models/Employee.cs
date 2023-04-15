namespace SportsCompetition.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; } 
       
        public ICollection<EmployeeEvent> EmployeeEvents { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
