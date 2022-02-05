using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using TimeTracking.Common.Constant;
using TimeTracking.Common.ExtensionMethod;
using TimeTracking.Common.Interfaces;

namespace TimeTracking.Web.Helper.Implementations
{
    public class CurrentUserService<TKey> : ICurrentUserService<TKey>
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var claimsPrincipal = httpContextAccessor?.HttpContext?.User;
            if (claimsPrincipal == null) return;

            UserId = claimsPrincipal.GetFromClaims<TKey>(AppValues.AppClaims.Id);
            FullName = claimsPrincipal.GetFromClaims<string>(AppValues.AppClaims.FullName);
            Principal = claimsPrincipal;
            IsAuthenticated = claimsPrincipal.Identity?.IsAuthenticated ?? false;
        }

        public TKey UserId { get; }
        public string FullName { get; }
        public IPrincipal Principal { get; set; }
        public bool IsAuthenticated { get; }
    }
}