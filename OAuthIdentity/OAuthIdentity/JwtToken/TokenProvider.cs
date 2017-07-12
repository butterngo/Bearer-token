//namespace OAuthIdentity.JwtToken
//{
//    using Microsoft.Owin.Security;
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Web;

//    public class TokenProvider : ISecureDataFormat<AuthenticationTicket>
//    {
//        private readonly string algorithm;

//        private readonly TokenValidationParameters validationParameters;

//        private readonly TokenProviderOptions _options;

//        public TokenProvider(string algorithm, TokenValidationParameters validationParameters, TokenProviderOptions options)
//        {
//            this.algorithm = algorithm;
//            this.validationParameters = validationParameters;
//            this._options = options;
//        }

//        public AuthenticationTicket Unprotect(string protectedText)
//            => Unprotect(protectedText, null);

//        public AuthenticationTicket Unprotect(string protectedText, string purpose)
//        {
//            //https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/blob/master/src/System.IdentityModel.Tokens.Jwt/JwtSecurityTokenHandler.cs
//            var handler = new JwtSecurityTokenHandler();
//            ClaimsPrincipal principal = null;
//            SecurityToken validToken = null;

//            try
//            {
//                principal = handler.ValidateToken(protectedText, this.validationParameters, out validToken);

//                var validJwt = validToken as JwtSecurityToken;

//                if (validJwt == null)
//                {
//                    throw new ArgumentException("Invalid JWT");
//                }

//                if (!validJwt.Header.Alg.Equals(algorithm, StringComparison.Ordinal))
//                {
//                    throw new ArgumentException($"Algorithm must be '{algorithm}'");
//                }

//            }
//            catch (SecurityTokenValidationException)
//            {
//                return null;
//            }
//            catch (ArgumentException)
//            {
//                return null;
//            }

//            return new AuthenticationTicket(principal, new AuthenticationProperties(), "Blog-Cookie");
//        }

//        public string Protect(AuthenticationTicket data)
//        {
//            throw new NotImplementedException();
//        }

//        public string Protect(AuthenticationTicket data, string purpose)
//        {
//            var identity = GenerateClaimsIdentity(data.Principal);

//            var now = DateTime.UtcNow;
//            var jwt = new JwtSecurityToken(
//                 issuer: _options.Issuer,
//                 audience: _options.Audience,
//                 claims: identity.Claims,
//                 notBefore: now,
//                 expires: now.Add(_options.Expiration),
//                 signingCredentials: _options.SigningCredentials);
//            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

//            return encodedJwt;
//        }

//        private ClaimsIdentity GenerateClaimsIdentity(ClaimsPrincipal principal)
//        {
//            var userId = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
//            var WebApiUserManager = ResolverFactory.GetService<WebApiUserManager>();

//            return AsyncHelper.RunSync(() => WebApiUserManager.CreateIdentityAsync(userId, "Bearer"));
//        }
//    }
//}