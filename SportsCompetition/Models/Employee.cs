namespace SportsCompetition.Models
{
    public class Employee
    {
        public readonly string[] Roles = { "Administrator", "Secretary", "Assistant", "Judge" };
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } 

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<EmployeeEvent> EmployeeEvents { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<Event> Events { get; set; }
    }
}
