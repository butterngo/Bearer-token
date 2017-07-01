using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using OAuthIdentity.Models;
using OAuthIdentity.Provider;
using OAuthIdentity.Repository;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(OAuthIdentity.Startup))]
namespace OAuthIdentity
{
    
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(OAuthIdentityContext.Create);

            app.CreatePerOwinContext<OAuthIdentityUserManager>(OAuthIdentityUserManager.Create);

            app.CreatePerOwinContext<IRefreshTokenRepository>(RefreshTokenRepository.Create);

            app.CreatePerOwinContext<IClientRepository>(ClientRepository.Create);

            app.CreatePerOwinContext<IClientUserRepository>(ClientUserRepository.Create);

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(20),
                TokenEndpointPath = new PathString("/token"),
                Provider = new ApplicationOAuthProvider(),
                RefreshTokenProvider = new ApplicationRefreshTokenProvider(),
                AllowInsecureHttp = true,
            };

            app.UseOAuthBearerTokens(OAuthServerOptions);

        }
    }
}