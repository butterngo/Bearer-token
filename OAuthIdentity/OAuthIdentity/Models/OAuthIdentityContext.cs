namespace OAuthIdentity.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;

    public class OAuthIdentityContext: IdentityDbContext<User, Role, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        public OAuthIdentityContext()
            : base("name=OAuthIdentity")
        {
        }

        public static OAuthIdentityContext Create()
        {
            return new OAuthIdentityContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users", "dbo");
            modelBuilder.Entity<Role>().ToTable("Roles", "dbo");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles", "dbo");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims", "dbo");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins", "dbo");
        }

        public virtual IDbSet<RefreshToken> RefreshTokens { get; set; }
    }
}