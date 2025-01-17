﻿using System.Security.Principal;

namespace TimeTracking.Common.Interfaces
{
    public interface ICurrentUserService<out TKey>
    {
        TKey UserId { get; }
        string FullName { get; }
        IPrincipal Principal { get; }
        bool IsAuthenticated { get; }
    }
}