using Microsoft.AspNetCore.Identity;

namespace SportsCompetition.Models
{
    public class User : IdentityUser<Guid>
    {
        public Employee Employee { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
