using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using TimeTracking.Common.Constant;
using TimeTracking.Web.Security.Entities;

namespace TimeTracking.Web.Security.Implementations
{
    public class AdditionalUserClaimsPrincipalFactory
        : UserClaimsPrincipalFactory<User, Role>
    {
        public AdditionalUserClaimsPrincipalFactory(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)principal.Identity;

            if (identity == null) return principal;
            var claims = new List<Claim>
            {
                new(AppValues.AppClaims.Id, user.Id.ToString()),
                new(AppValues.AppClaims.FullName, user.FullName),
            };

            identity.AddClaims(claims);
            return principal;
        }
    }
}