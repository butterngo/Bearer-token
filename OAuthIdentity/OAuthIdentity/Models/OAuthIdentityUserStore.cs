namespace OAuthIdentity.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;

    public class OAuthIdentityUserStore : UserStore<User, Role, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, IDisposable
    {
        public OAuthIdentityUserStore(DbContext context) : base(context)
        {
        }
    }
}