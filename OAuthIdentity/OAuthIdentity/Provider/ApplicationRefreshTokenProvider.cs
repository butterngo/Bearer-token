namespace OAuthIdentity.Provider
{
    using Microsoft.Owin.Security.Infrastructure;
    using System;
    using System.Threading.Tasks;
    using OAuthIdentity.Repository;
    using Microsoft.AspNet.Identity.Owin;
    using OAuthIdentity.Models;
    using System.Security.Cryptography;
    using Microsoft.AspNet.Identity;

    public class ApplicationRefreshTokenProvider : IAuthenticationTokenProvider
    {
        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            if (context.Ticket == null)
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                context.Response.ReasonPhrase = "invalid refresh token";
                return;
            }

            var repository = context.OwinContext.Get<IRefreshTokenRepository>();

            string subject = context.Ticket.Identity.GetUserId();

            await repository.DeleteAsync(x => x.Subject.Equals(subject));

            var token = new RefreshToken()
            {
                Subject = subject,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            };

            context.Ticket.Properties.IssuedUtc = token.IssuedUtc;

            context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

            token.ProtectedTicket = context.SerializeTicket();

            var result = await repository.AddAsync(token);

            context.SetToken(result.Id);
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var repository = context.OwinContext.Get<IRefreshTokenRepository>();

            var token = await repository.FindByAsync(context.Token);

            if (token != null)
            {
                context.DeserializeTicket(token.ProtectedTicket);

                await repository.DeleteAsync(token.Id);
            }
        }
    }
}