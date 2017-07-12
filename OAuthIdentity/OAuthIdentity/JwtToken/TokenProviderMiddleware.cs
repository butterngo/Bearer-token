namespace OAuthIdentity.JwtToken
{
    using Microsoft.Owin;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using Microsoft.AspNetCore.Http;

    public class TokenProviderMiddleware : OwinMiddleware
    {
        private readonly TokenProviderOptions _options;

        public TokenProviderMiddleware(OwinMiddleware next, TokenProviderOptions options) : base(next)
        {
            _options = options;
        }

        public override async Task Invoke(IOwinContext context)
        {
            if (!context.Request.Path.Equals(_options.Path))
            {
                await Next.Invoke(context);
            }
            else
            {
                if (!context.Request.Method.Equals("POST"))
                {
                    context.Response.StatusCode = 400;

                    await context.Response.WriteAsync("Bad request.");
                }

                var result = await context.Request.ReadFormAsync();

                var aa = result["userId"];

                context.Response.ContentType = "application/json";

               await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                   access_token="aaa",
                   expires_in = 360
               }, new JsonSerializerSettings { Formatting = Formatting.Indented }));

            }
        }
    }
}