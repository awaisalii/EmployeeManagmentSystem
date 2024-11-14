using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;
namespace BuisnessLayer.MiddleWare
{
    public class SetUserIdentifierMiddleware
    {
        private readonly RequestDelegate _next;

        public SetUserIdentifierMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var authenticatedIdentity = context.User.Identities.FirstOrDefault(identity => identity.IsAuthenticated);

            if (authenticatedIdentity != null)
            {
                var userId = authenticatedIdentity.FindFirst("Id")?.Value;  
                if (!string.IsNullOrEmpty(userId))
                {
                    {
                        var secondIdentity = context.User.Identities.ElementAtOrDefault(0);
                        if (secondIdentity != null)
                        {
                            var claimsPrincipal = new ClaimsPrincipal(context.User.Identities.Where(i => i != secondIdentity));
                            context.User = claimsPrincipal;
                        }
                    }
                    context.User.AddIdentity(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, userId) }));
                }
            }
            await _next(context);
        }

    }
}
