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
        public CustomAuthorizeAttribute(Role role, Role role1)
        {
            Roles = role.ToString() +", " + role1.ToString();
        }
        public CustomAuthorizeAttribute(Role role, Role role1, Role role2)
        {
            Roles = role.ToString() + ", " + role1.ToString() + ", " + role2.ToString();
        }
        public CustomAuthorizeAttribute(Role role, Role role1, Role role2, Role role3)
        {
            Roles = role.ToString() + ", " + role1.ToString() + ", " + role2.ToString() + ", " + role3.ToString();
        }
    }
}
