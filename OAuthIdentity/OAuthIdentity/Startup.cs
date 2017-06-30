using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using OAuthIdentity.Models;
using OAuthIdentity.Provider;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartupAttribute(typeof(OAuthIdentity.Startup))]

namespace OAuthIdentity
{
    
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(OAuthIdentityContext.Create);

            app.CreatePerOwinContext<OAuthIdentityUserManager>(OAuthIdentityUserManager.Create);
         
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(20),
                TokenEndpointPath = new PathString("/token"),
                Provider = new ApplicationOAuthProvider(),
                AllowInsecureHttp = true,
            };

            app.UseOAuthBearerTokens(OAuthServerOptions);

        }
    }
}