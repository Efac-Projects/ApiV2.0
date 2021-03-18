﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.Extension
{
    public static class ClaimsPrincipalExtensions
    {
        public static TId GetId<TId>(this ClaimsPrincipal principal)
        {
            if (principal == null || principal.Identity == null ||
                !principal.Identity.IsAuthenticated)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            var loggedInUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (typeof(TId) == typeof(string) ||
                typeof(TId) == typeof(int) ||
                typeof(TId) == typeof(long) ||
                typeof(TId) == typeof(Guid))
            {
                var converter = TypeDescriptor.GetConverter(typeof(TId));

                return (TId)converter.ConvertFromInvariantString(loggedInUserId);
            }

            throw new InvalidOperationException("The user id type is invalid.");
        }

        public static Guid GetId(this ClaimsPrincipal principal)
        {
            return principal.GetId<Guid>();
        }
    }
}

