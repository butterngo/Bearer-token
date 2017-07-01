namespace OAuthIdentity.Repository
{
    using OAuthIdentity.Models;

    public class RefreshTokenRepository : Repository<RefreshToken, string, OAuthIdentityContext>
        , IRefreshTokenRepository
    {
        public RefreshTokenRepository() : this(new OAuthIdentityContext()) { }

        public RefreshTokenRepository(OAuthIdentityContext context) : base(context)
        {
        }

        public static RefreshTokenRepository Create()
        {
            return new RefreshTokenRepository();
        }
    }
}