namespace OAuthIdentity.Migrations
{
    using Microsoft.AspNet.Identity;
    using OAuthIdentity.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OAuthIdentity.Models.OAuthIdentityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OAuthIdentity.Models.OAuthIdentityContext context)
        {
        }
    }
}
