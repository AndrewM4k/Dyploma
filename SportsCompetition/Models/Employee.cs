using Microsoft.AspNetCore.Identity;
using StackExchange.Redis;

namespace SportsCompetition.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public string Surname { get; set; }
        public Enums.Role Role { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public ICollection<EmployeeEvent> EmployeeEvents { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<StreamJudgeEmployee> StreamJudgeEmployees { get; set; }
        public ICollection<Stream> Streams { get; set; }
    }
}
