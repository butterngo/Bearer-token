namespace OAuthIdentity.JwtToken
{
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.Owin;
    using System;

    public class TokenProviderOptions
    {
        public PathString Path { get; set; } = new PathString("/token1");

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; } = TimeSpan.FromDays(+1);

        public SigningCredentials SigningCredentials { get; set; }
    }
}