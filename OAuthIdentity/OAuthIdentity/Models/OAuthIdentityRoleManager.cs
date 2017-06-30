namespace OAuthIdentity.Models
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using System;

    public class OAuthIdentityRoleManager : RoleManager<Role>, IDisposable
    {
        public OAuthIdentityRoleManager(IRoleStore<Role, string> store) : base(store)
        {
        }

        public static RoleManager<Role> Create(IdentityFactoryOptions<RoleManager<Role>> options, IOwinContext context)
        {
            var roleStore = context.Get<IRoleStore<Role>>();
            return new OAuthIdentityRoleManager(roleStore);
        }
    }
}