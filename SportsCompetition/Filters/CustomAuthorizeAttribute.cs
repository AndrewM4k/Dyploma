using Microsoft.AspNetCore.Authorization;
using SportsCompetition.Enums;

namespace SportsCompetition.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public CustomAuthorizeAttribute(Role role)
        {
            Roles = role.ToString();
        }
    }
}
