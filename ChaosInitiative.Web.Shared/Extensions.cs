using System;
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
    }
}