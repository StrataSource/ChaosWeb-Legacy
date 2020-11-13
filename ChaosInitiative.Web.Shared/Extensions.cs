using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;

namespace ChaosInitiative.Web.Shared
{
    public static class Extensions
    {
        public static IApplicationBuilder UsePhpRedirection(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<PhpRedirectionMiddleware>();
        }

        public static IEnumerable<T> GetOverlapping<T>(this IEnumerable<T> enumerable, IEnumerable<T> other)
        {
            return enumerable.Where(e1 => other.Any(e2 => e1.Equals(e2)));
        }

        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Count() == 0;
        }
    }
}