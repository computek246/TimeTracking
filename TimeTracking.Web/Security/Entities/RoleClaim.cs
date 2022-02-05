using Microsoft.AspNetCore.Identity;

namespace TimeTracking.Web.Security.Entities
{
    public class RoleClaim : IdentityRoleClaim<int>
    {
        public Role Role { get; set; }
    }
}