using Microsoft.AspNetCore.Identity;

namespace TimeTracking.Web.Security.Entities
{
    public class UserLogin : IdentityUserLogin<int>
    {
        public User User { get; set; }
    }
}