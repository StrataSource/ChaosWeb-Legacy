using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ChaosInitiative.Web.Shared
{
    public class PhpRedirectionMiddleware
    {
        private readonly RequestDelegate _next;

        public PhpRedirectionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            string requestPath = context.Request.Path.Value;
            if (requestPath.ToLower().Contains(".php"))
            {
                string fixedPath = requestPath.Replace(".php", "");
                context.Response.Redirect(fixedPath, true);
                return Task.CompletedTask;
            }

            return _next.Invoke(context);
        }
    }
}
