using Microsoft.AspNetCore.Identity;

namespace TimeTracking.Web.Security.Entities
{
    public class UserClaim : IdentityUserClaim<int>
    {
        public User User { get; set; }
    }
}