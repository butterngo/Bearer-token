namespace OAuthIdentity.Provider
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using OAuthIdentity.Enum;
    using OAuthIdentity.Models;
    using OAuthIdentity.Repository;
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Data.Entity;
    using System.Security.Principal;
    using System.Linq;

    public class ApplicationOAuthProvider: OAuthAuthorizationServerProvider
    {
        ////https://docs.microsoft.com/en-us/aspnet/aspnet/overview/owin-and-katana/owin-oauth-20-authorization-server

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;

            string clientSecret;

            if (context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                try
                {
                    var clientRepository = context.OwinContext.Get<IClientRepository>();

                    Client client = await clientRepository.FindByAsync(clientId);

                    if (client != null && client.Secret == clientSecret)
                    {
                        context.Options.AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(client.TokenLifeTime);
               
                        context.OwinContext.Set<Client>("oauth:client", client);

                        context.Validated(clientId);
                    }
                    else
                    {
                        context.SetError("invalid_client", "Client credent``ials are invalid.");
                        context.Rejected();
                    }
                }
                catch
                {
                    context.SetError("server_error");
                    context.Rejected();
                }
            }
            else
            {
                context.SetError(
                    "invalid_client",
                    "Client credentials could not be retrieved through the Authorization header.");

                context.Rejected();
            }
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            Client client = context.OwinContext.Get<Client>("oauth:client");

            if (client.AllowedGrant == OAuthGrant.ResourceOwner)
            {
                var userManager = context.OwinContext.GetUserManager<OAuthIdentityUserManager>();

                var user = await userManager.FindAsync(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }

                var clientUserRepository = context.OwinContext.Get<IClientUserRepository>();

                var clientUser = await clientUserRepository
                    .Find(c => c.ClientId.Equals(client.Id) && c.UserId.Equals(user.Id))
                    .FirstOrDefaultAsync();

                if (clientUser == null)
                {
                    context.SetError("client_user", "User is not belong this client.");
                    return;
                }

                ClaimsIdentity oAuthIdentity = await userManager.CreateIdentityAsync(user, "Bearer");

                var propertyDictionary = new Dictionary<string, string> { { "userName", user.UserName } };

                var properties = new AuthenticationProperties(propertyDictionary);

                AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);

                context.Validated(ticket);
            }
            else
            {
                context.SetError(
                   "invalid_grant",
                   "Client is not allowed for the 'Resource Owner Password Credentials Grant'");

                context.Rejected();
            }
        }

        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            var identity = new ClaimsIdentity(new GenericIdentity(
            context.ClientId, OAuthDefaults.AuthenticationType),
            context.Scope.Select(x => new Claim("urn:oauth:scope", x))
            );

             context.Validated(identity);

            return Task.FromResult(0);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);

            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }
    }
}