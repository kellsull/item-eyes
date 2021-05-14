using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemEyes
{
    public class SwaggerAuthorizedMiddleware
    {
        private readonly RequestDelegate _next;

        public SwaggerAuthorizedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/api")
                && !context.User.Identity.IsAuthenticated)
            {
                context.Response.Redirect("/Identity/Accounts/Login");
                return;
            }

            await _next.Invoke(context);
        }
    }

    public static class SwaggerAuthorizeExtensions
    {
        public static IApplicationBuilder UseSwaggerAuthorized(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SwaggerAuthorizedMiddleware>();
        }
    }
}
