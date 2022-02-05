﻿using Microsoft.AspNetCore.Identity;

namespace TimeTracking.Web.Security.Entities
{
    public class UserRole : IdentityUserRole<int>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}