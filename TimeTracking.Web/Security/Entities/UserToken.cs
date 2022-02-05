using Microsoft.AspNetCore.Identity;

namespace TimeTracking.Web.Security.Entities
{
    public class UserToken : IdentityUserToken<int>
    {
        public User User { get; set; }
    }
}