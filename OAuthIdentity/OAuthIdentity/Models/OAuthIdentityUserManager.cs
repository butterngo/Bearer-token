namespace OAuthIdentity.Models
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class OAuthIdentityUserManager : UserManager<User>, IDisposable
    {
        public OAuthIdentityUserManager(IUserStore<User> store) : base(store) { }

        public static OAuthIdentityUserManager Create(IdentityFactoryOptions<OAuthIdentityUserManager> options,
           IOwinContext context)
        {
            var manager = new OAuthIdentityUserManager(new UserStore<User>(context.Get<OAuthIdentityContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<User>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            return manager;
        }

        public override Task<ClaimsIdentity> CreateIdentityAsync(User user, string authenticationType)
        {
            IList<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id, null, ClaimsIdentity.DefaultIssuer, "Provider"));

            claims.Add(new Claim("Email", user.Email, null, ClaimsIdentity.DefaultIssuer, "Provider"));

            claims.Add(new Claim(ClaimTypes.Name, user.Email, null, ClaimsIdentity.DefaultIssuer, "Provider"));
           
            var claimsIdentity = new ClaimsIdentity(claims, authenticationType);

            return Task.FromResult(claimsIdentity);
        }
    }
}