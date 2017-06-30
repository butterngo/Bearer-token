namespace OAuthIdentity.Provider
{
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using OAuthIdentity.Models;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class ApplicationOAuthProvider: OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();

            return Task.FromResult(0);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<OAuthIdentityUserManager>();

            var user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            ClaimsIdentity oAuthIdentity = await userManager.CreateIdentityAsync(user, "Bearer");

            var propertyDictionary = new Dictionary<string, string> { { "userName", user.UserName } };

            var properties = new AuthenticationProperties(propertyDictionary);

            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            
            context.Validated(ticket);
        }
    }
}