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
            // Find the authenticated identity
            var authenticatedIdentity = context.User.Identities.FirstOrDefault(identity => identity.IsAuthenticated);

            if (authenticatedIdentity != null)
            {
                // Extract the userId from the authenticated identity
                var userId = authenticatedIdentity.FindFirst("Id")?.Value;  // Adjust claim type if needed

                if (!string.IsNullOrEmpty(userId))
                {
                    // Remove the second identity at index 0 if it exists
                    //if (context.User.Identities.Count > 1)
                    {
                        var secondIdentity = context.User.Identities.ElementAtOrDefault(0);
                        if (secondIdentity != null)
                        {
                            // Remove the identity from the context
                            var claimsPrincipal = new ClaimsPrincipal(context.User.Identities.Where(i => i != secondIdentity));
                            context.User = claimsPrincipal;
                        }
                    }

                    // Manually set the UserIdentifier using the authenticated identity
                    context.User.AddIdentity(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, userId) }));
                }
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }

    }
}
