using Microsoft.AspNetCore.Identity;

namespace SportsCompetition.Models
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public IdentityUser<Guid> User { get; set; }
    }
}
